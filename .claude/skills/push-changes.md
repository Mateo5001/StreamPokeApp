---
name: push-changes
description: Stage, commit, and push changes to the remote repository with conventional commits
---

# Push Changes Skill

This skill helps you stage, commit, and push changes following conventional commit format.

## Usage

```
/push-changes [message]
```

If no message is provided, you'll be prompted to enter one.

## Workflow

1. **Check status** - See what files are modified, deleted, or untracked
2. **Stage changes** - Add all changes (or selectively)
3. **Create commit** - Use conventional commit format (feat:, fix:, chore:, docs:, refactor:)
4. **Push to remote** - Push to the current branch
5. **Optional: Create PR** - If pushing to a feature branch, offer to create a PR

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

## Examples

```
/push-changes "feat: add JWT authentication endpoints"
/push-changes "fix: resolve refresh token rotation bug"
/push-changes "chore: update EF Core to latest version"
/push-changes "docs: update API documentation"
```

## Current Branch

The skill uses the current git branch. Make sure you're on the correct branch before running.

## Implementation

```bash
# 1. Check status
git status

# 2. Stage all changes (or use -p for selective)
git add -A

# 3. Commit with conventional format
git commit -m "<type>: <message>"

# 4. Push to origin
git push origin <current-branch>

# 5. Optional: Create PR (if not on main/develop)
gh pr create --title "<type>: <message>" --body "Description of changes"
```