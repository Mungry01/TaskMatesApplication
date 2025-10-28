# Authentication System Implementation

## Overview
A complete authentication system has been implemented for TaskMates with file-based storage for users and events.

## Features Implemented

### 1. Role Selection Landing Page
- **File**: `TaskMates/Views/Home/RoleSelection.cshtml`
- First page users see when visiting the site
- Users select their role (Student, Faculty, or Administrator)
- Beautiful UI with role-specific cards and colors

### 2. Login System
- **File**: `TaskMates/Views/Home/Login.cshtml`
- Secure login with username and password
- Role-based authentication (validates role matches selection)
- Error handling for invalid credentials
- Links to registration and role change

### 3. Registration System
- **File**: `TaskMates/Views/Home/Register.cshtml`
- Complete registration form with validation
- Fields: Username, Password, Confirm Password, Full Name, Email, Position
- Password matching validation
- Username uniqueness check
- Automatic role assignment based on selection

### 4. User Management with File Storage
- **File**: `TaskMates/Services/FileStorageService.cs`
- User CRUD operations (Create, Read, Update, Delete)
- Users stored in `TaskMates/Data/Storage/users.json`
- Username/password validation
- User lookup by username or ID

### 5. Updated UserProfile Model
- **File**: `TaskMates/Models/UserProfile.cs`
- Added `Username` and `Password` fields for authentication
- Maintains role-specific fields (Student, Faculty, Admin)

### 6. File-Based Data Service
- **File**: `TaskMates/Services/FileBasedDataService.cs`
- Implements `IDataService` with persistent file storage
- Events are saved to `TaskMates/Data/Storage/events.json`
- Ideas are saved to `TaskMates/Data/Storage/ideas.json`
- Full CRUD operations for Events and Ideas
- All changes persist across application restarts

### 7. Session Management
- **File**: `TaskMates/Program.cs`
- Session support configured (30-minute timeout)
- User info stored in session (UserId, Username, UserName, UserRole, UserEmail)
- Secure session cookies

### 8. Updated Controllers
- **File**: `TaskMates/Controllers/HomeController.cs`
- Login/Register POST and GET actions
- Session-based authentication checks on all protected pages
- Automatic redirect to RoleSelection if not authenticated
- Logout functionality

### 9. Updated Layout
- **File**: `TaskMates/Views/Shared/_Layout.cshtml`
- Displays logged-in user's name and role with icon
- Logout button when authenticated
- Login button when not authenticated
- Role badge with appropriate color coding

### 10. Updated Views
- **Files**: `Index.cshtml`, `Profile.cshtml`, `Ideas.cshtml`, `Events.cshtml`
- Removed localStorage dependencies
- Now use server-side session data
- Better security and data consistency

## Authentication Flow

1. **User visits site** → Redirected to Role Selection page
2. **User selects role** → Proceeds to Login page with role pre-selected
3. **User clicks "Sign Up"** → Registration page with role pre-selected
4. **User registers** → Account created in `users.json` → Redirected to Login
5. **User logs in** → Credentials validated → Session created → Dashboard access
6. **User navigates** → Session maintained across all pages
7. **User logs out** → Session cleared → Redirected to Role Selection

## Default Route
- Changed from `Home/Index` to `Home/RoleSelection`
- Ensures all users must authenticate before accessing the application

## File Storage
All data is persisted in JSON files located in `TaskMates/Data/Storage/`:
- `users.json` - User accounts with credentials and roles
- `events.json` - Events with full CRUD support
- `ideas.json` - Ideas with full CRUD support

## Security Notes
⚠️ **Important**: In this implementation, passwords are stored in plain text. For production use, you should:
1. Hash passwords using BCrypt or similar
2. Add password strength requirements
3. Implement password reset functionality
4. Add CSRF protection
5. Enable HTTPS only
6. Add rate limiting for login attempts

## Testing the System

### Create a Test User
1. Run the application
2. Select a role (e.g., Student)
3. Click "Sign Up"
4. Fill in the registration form:
   - Username: testuser
   - Password: password123
   - Name: Test User
   - Email: test@example.com
5. Submit and login

### Test CRUD Operations for Events
1. Login as a user
2. Navigate to Events page
3. Create a new event (will be saved to events.json)
4. Edit the event (changes persisted)
5. Delete the event (removed from events.json)
6. Refresh the page - changes are permanent

## Files Modified
- ✅ `TaskMates/Models/UserProfile.cs` - Added authentication fields
- ✅ `TaskMates/Services/FileStorageService.cs` - Added user CRUD operations
- ✅ `TaskMates/Services/FileBasedDataService.cs` - NEW FILE - File-based data service
- ✅ `TaskMates/Controllers/HomeController.cs` - Complete authentication logic
- ✅ `TaskMates/Program.cs` - Session support and route change
- ✅ `TaskMates/Views/Home/RoleSelection.cshtml` - NEW FILE - Landing page
- ✅ `TaskMates/Views/Home/Login.cshtml` - NEW FILE - Login page
- ✅ `TaskMates/Views/Home/Register.cshtml` - NEW FILE - Registration page
- ✅ `TaskMates/Views/Shared/_Layout.cshtml` - User info display
- ✅ `TaskMates/Views/Home/Index.cshtml` - Session-based data
- ✅ `TaskMates/Views/Home/Profile.cshtml` - Session-based data
- ✅ `TaskMates/Views/Home/Ideas.cshtml` - Session-based data
- ✅ `TaskMates/Views/Home/Events.cshtml` - Session-based data

## Next Steps
To run the application:
```bash
dotnet run --project TaskMates
```

The application will:
1. Initialize sample data for ideas and events
2. Start with an empty users list (you'll need to register)
3. Listen on the configured ports (check launchSettings.json)

Visit the application URL and you'll be greeted with the Role Selection page!

