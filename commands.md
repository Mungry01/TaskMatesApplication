# show commits from the last day (works with today's date = Oct 28, 2025)
git log --since="1 day ago" --oneline --decorate
# or inspect history for a speci	fic file
git log --since="1 day ago" -- path/to/file
