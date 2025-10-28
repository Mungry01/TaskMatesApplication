# Faculty Endorsement & UI Fixes

## Issues Fixed

### 1. ✅ Faculty Can Only Endorse, Not Volunteer
**Problem**: Faculty members had access to the volunteer button on ideas, which didn't align with their role.

**Solution**: 
- Modified `Ideas.cshtml` to hide the volunteer button for faculty members
- Added conditional logic: `${currentUser.role !== 'faculty' ? ... : ''}`
- Faculty now only see the Like and Endorse buttons

**Files Changed**:
- `TaskMates/Views/Home/Ideas.cshtml` (lines 256-260)

### 2. ✅ Toggle Endorsement (Endorse/Cancel)
**Problem**: Once a faculty member endorsed an idea, they couldn't cancel their endorsement.

**Solution**:
- Updated backend `EndorseIdeaAsync` method to toggle endorsement
- If faculty member already endorsed the idea, clicking endorses cancels it
- If not endorsed or endorsed by someone else, clicking endorses it

**Backend Changes**:
- `TaskMates/Services/FileBasedDataService.cs`:
  ```csharp
  if (idea.EndorsedBy == facultyName)
  {
      idea.EndorsedBy = null; // Cancel endorsement
  }
  else
  {
      idea.EndorsedBy = facultyName; // Endorse
  }
  ```

**Frontend Changes**:
- `TaskMates/Views/Home/Ideas.cshtml` (lines 261-265):
  - Button shows "✅ Endorse" when idea is not endorsed by current faculty
  - Button shows "❌ Cancel Endorsement" when idea is endorsed by current faculty
  - Button color changes: green for endorse, yellow/warning for cancel
  - Button dynamically checks: `${idea.endorsedBy === currentUser.name ? ... : ...}`

**Files Changed**:
- `TaskMates/Services/FileBasedDataService.cs`
- `TaskMates/Services/InMemoryDataService.cs` (consistency)
- `TaskMates/Views/Home/Ideas.cshtml`

### 3. ✅ Fixed Extra Number Above Likes in Events
**Problem**: Events page showed a status badge in one div and likes in another div, making it look like there was a random number above the likes count.

**Solution**:
- Consolidated the status badge and likes/endorsement badges into the same div
- Removed the separate div for likes
- Added `ms-1` (margin-start) classes for proper spacing

**Before**:
```html
<div class="mb-2">
    <span class="status-badge">Approved</span>
</div>
<div class="mb-2">
    <span class="badge">❤️ 15</span>
</div>
```

**After**:
```html
<div class="mb-2">
    <span class="status-badge">Approved</span>
</div>
<div class="mb-2">
    <span class="badge">❤️ 15</span>
    <span class="badge ms-1">✅ Endorsed</span>
</div>
```

**Files Changed**:
- `TaskMates/Views/Home/Events.cshtml` (lines 248-257)

## Testing the Changes

### Test Faculty Endorsement Toggle:
1. Login as a faculty member
2. Go to Ideas page
3. Click "✅ Endorse" on an idea → Button changes to "❌ Cancel Endorsement" (yellow)
4. Click "❌ Cancel Endorsement" → Button changes back to "✅ Endorse" (green)
5. Notice that volunteer button is **not visible** for faculty

### Test Faculty Cannot Volunteer:
1. Login as a faculty member
2. Go to Ideas page
3. Confirm volunteer button is hidden
4. Login as a student
5. Verify volunteer button is visible for students

### Test Events Display:
1. Go to Events page
2. Verify status badge (Approved/Planned/etc.) appears cleanly
3. Verify likes count appears below status, not stacked awkwardly
4. Verify endorsement badges appear on the same line as likes

## Summary

All three issues have been resolved:
- ✅ Faculty can only endorse ideas (no volunteer button)
- ✅ Faculty can toggle their endorsement (endorse/cancel)
- ✅ Events display properly without extra numbers

The application now properly enforces role-based permissions and displays information cleanly!

