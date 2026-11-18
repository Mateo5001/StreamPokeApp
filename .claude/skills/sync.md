---
name: sync
description: Sync with remote - commit, fetch, pull (handle conflicts), push, and summarize changes
---

# Sync Skill

This skill synchronizes your local branch with the remote repository by committing changes, fetching, pulling (with conflict handling), pushing, and providing a summary.

## Usage

```
/sync [commit-message]
```

If no commit message is provided, you'll be prompted to enter one using conventional commit format.

## Workflow

1. **Check status** - Show current changes (modified, deleted, untracked)
2. **Stage & Commit** - Add all changes and commit with conventional format
3. **Fetch** - Get latest changes from remote
4. **Pull** - Merge remote changes (handle conflicts if any)
5. **Push** - Push local commits to remote
6. **Summarize** - Show summary of what was synced

## Conflict Handling

If conflicts arise during pull:
- The skill will pause and show conflicted files
- You'll be asked how to proceed (resolve manually, abort, or use theirs/ours)
- After resolution, continue with push

## Conventional Commit Types

| Type | Description |
|------|-------------|
| `feat:` | New feature |
| `fix:` | Bug fix |
| `chore:` | Maintenance, build, deps |
| `docs:` | Documentation changes |
| `refactor:` | Code restructuring |
| `style:` | Formatting, no logic change |
| `test:` | Adding tests |
| `perf:` | Performance improvements |

## Implementation

```bash
# 1. Check status
git status

# 2. Stage all changes
git add -A

# 3. Commit with message
git commit -m "<message>"

# 4. Fetch from remote
git fetch origin

# 5. Pull with rebase (or merge)
git pull origin <current-branch> --rebase
# If conflicts: handle them, then git rebase --continue

# 6. Push to remote
git push origin <current-branch>

# 7. Show summary
git log --oneline -5
git diff HEAD~1 --stat
```