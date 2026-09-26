<#
PowerShell-скрипт: disable-vs-tab-snippets.ps1
Описание: Пытается удалить привязки клавиш для команды Edit.InsertSnippet из локальных .vssettings
и — опционально — импортирует изменённый файл настроек в Visual Studio через devenv.exe /importsettings.

Примечания и риски:
- Скрипт редактирует .vssettings как текст/регулярными выражениями. Рекомендуется закрыть Visual Studio перед запуском.
- Всегда создаётся резервная копия (*.bak).
- Не гарантировано для всех версий Visual Studio, но часто удаляет настройки привязок сниппетов.
#>

Param(
	[switch]$ImportIntoVS,    # если установлен — попытается импортировать изменённые файлы через devenv.exe
	[string]$DevenvPath        # если указано, будет использован указанный путь к devenv.exe
)

Write-Output "GitHub Copilot: запускаю скрипт удаления привязок Edit.InsertSnippet из .vssettings (не забудьте закрыть Visual Studio)."

$patternSingle = '<[^>]*CommandName\s*=\s*"[^"]*InsertSnippet[^"]*"[^>]*\/>'
$patternBlock = '<KeyBindings[^>]*>.*?InsertSnippet.*?<\/KeyBindings>'

$docFolders = Get-ChildItem -Path (Join-Path $env:USERPROFILE 'Documents') -Directory | Where-Object { $_.Name -like 'Visual Studio*' }
if (-not $docFolders) {
	Write-Warning "Папки 'Documents\Visual Studio*' не найдены. Проверьте расположение .vssettings вручную."
}

$modified = @()
foreach ($folder in $docFolders) {
	$settingsDir = Join-Path $folder.FullName 'Settings'
	if (-not (Test-Path $settingsDir)) { continue }

	$files = Get-ChildItem -Path $settingsDir -Filter '*.vssettings' -File -ErrorAction SilentlyContinue
	foreach ($file in $files) {
		try {
			$text = Get-Content -Raw -LiteralPath $file.FullName -ErrorAction Stop
			$orig = $text

			# Удаляем одиночные теги, содержащие InsertSnippet
			$text = [regex]::Replace($text, $patternSingle, '', 'Singleline')
			# Удаляем блоки KeyBindings, содержащие InsertSnippet
			$text = [regex]::Replace($text, $patternBlock, '', 'Singleline')

			if ($text -ne $orig) {
				$bak = $file.FullName + '.bak'
				Copy-Item -LiteralPath $file.FullName -Destination $bak -Force
				Set-Content -LiteralPath $file.FullName -Value $text -Force
				Write-Output "Изменён: $($file.FullName) (резервная копия: $bak)"
				$modified += $file.FullName

				if ($ImportIntoVS) {
					$devenv = $DevenvPath
					if (-not $devenv) {
						# Попробуем найти devenv в Program Files
						$candidates = @(
							"$env:ProgramFiles(x86)\Microsoft Visual Studio\*\Professional\Common7\IDE\devenv.exe",
							"$env:ProgramFiles(x86)\Microsoft Visual Studio\*\Enterprise\Common7\IDE\devenv.exe",
							"$env:ProgramFiles\Microsoft Visual Studio\*\Professional\Common7\IDE\devenv.exe",
							"$env:ProgramFiles\Microsoft Visual Studio\*\Enterprise\Common7\IDE\devenv.exe"
						)
						foreach ($pat in $candidates) {
							$found = Get-ChildItem -Path $pat -File -ErrorAction SilentlyContinue | Select-Object -First 1
							if ($found) { $devenv = $found.FullName; break }
						}
					}

					if ($devenv -and (Test-Path $devenv)) {
						Write-Output "Импорт настроек в Visual Studio через: $devenv"
						& "$devenv" /importsettings `"$($file.FullName)`" | Out-Null
						Write-Output "Импорт для $($file.Name) выполнен (если Visual Studio закрыта)."
					} else {
						Write-Warning "devenv.exe не найден автоматически. Укажите путь в параметре -DevenvPath или импортируйте вручную."
					}
				}
			} else {
				Write-Output "Без изменений: $($file.FullName)"
			}
		} catch {
			Write-Warning "Ошибка при обработке $($file.FullName): $_"
		}
	}
}

if (-not $modified) {
	Write-Output "Нет найденных изменений. Возможно, настройки хранятся в другом месте или уже изменены вручную."
} else {
	Write-Output "Готово. Изменено файлов: $($modified.Count)"
}

Write-Output "Рекомендация: перезапустите Visual Studio и проверьте поведение Tab для вставки сниппетов. Если что-то пошло не так, восстановите файл .bak."