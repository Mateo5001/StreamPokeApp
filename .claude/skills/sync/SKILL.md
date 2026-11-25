---
name: sync
description: Sincroniza el repositorio con el remoto - commit de cambios locales, fetch, pull (resolviendo conflictos con el usuario) y push. Usar solo cuando el usuario invoque /sync.
disable-model-invocation: true
allowed-tools: Bash(git *)
---

# /sync

Sincroniza la rama actual con el remoto siguiendo estos pasos en orden. Si un paso falla, detente y reporta el error; no continúes al siguiente.

## 1. Commit de los cambios actuales

1. Ejecuta `git status --short` y `git diff --stat`.
2. Si no hay cambios (working tree limpio), salta al paso 2.
3. Si hay cambios:
   - Haz `git add -A`.
   - Genera un mensaje usando **conventional commits** (`feat:`, `fix:`, `chore:`, `docs:`, `refactor:`) basado en el diff real. Una línea de resumen, y cuerpo breve solo si hace falta.
   - Termina el mensaje con la línea de atribución indicada en el contexto de la conversación (Co-Authored-By), si existe.
   - Ejecuta `git commit`.
   - Nunca uses `--no-verify`. Si un hook falla, corrige la causa y reporta.
   - Si ves archivos que parezcan secretos (`.env`, `appsettings*.json` con credenciales reales, claves), no los incluyas y avisa al usuario.

## 2. Fetch

Ejecuta `git fetch --prune`. Muestra brevemente cuántos commits adelante/atrás está la rama respecto a su upstream (`git status -sb`).

## 3. Pull

1. Ejecuta `git pull --no-rebase` (merge) sobre la rama actual.
2. Si termina sin conflictos, continúa al paso 4.
3. **Si hay conflictos:**
   - Lista los archivos en conflicto con `git diff --name-only --diff-filter=U`.
   - Para **cada archivo**, muestra los bloques en conflicto (sección `<<<<<<<` / `=======` / `>>>>>>>`) explicando qué cambió cada lado (local vs remoto).
   - Pregunta al usuario qué hacer con cada conflicto usando AskUserQuestion. Opciones típicas: conservar la versión local, conservar la remota, combinar ambas, o resolver manualmente (indicando cómo).
   - Aplica la decisión, quita los marcadores de conflicto, ejecuta `git add <archivo>`.
   - Cuando todos estén resueltos, ejecuta `git commit --no-edit` para concluir el merge.
   - Si el usuario prefiere cancelar, ejecuta `git merge --abort` y detente.

## 4. Push

1. Ejecuta `git push`. Si la rama no tiene upstream, usa `git push -u origin <rama>`.
2. Nunca uses `--force` ni `--force-with-lease` salvo que el usuario lo pida explícitamente. Si el push es rechazado, reporta el motivo.

## Resumen final

Termina con un resumen corto: commit creado (hash + mensaje, o "sin cambios locales"), commits traídos del remoto, conflictos resueltos (si hubo) y resultado del push.
