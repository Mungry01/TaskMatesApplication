# TaskMates - Quick Start Guide

## 🚀 Running the Application

### Option 1: Visual Studio
1. Open `TaskMatesApplication.sln` in Visual Studio 2022
2. Press `F5` or click the "Run" button
3. The application will open in your default browser

### Option 2: Command Line
```bash
cd TaskMates
dotnet run
```
Then navigate to `https://localhost:5001` in your browser

### Option 3: Visual Studio Code
1. Open the `TaskMatesApplication` folder in VS Code
2. Press `F5` to start debugging
3. Or use the terminal: `cd TaskMates && dotnet run`

## 👤 Testing Different User Roles

### Testing as a Student
1. On the login page, click the **Student** card
2. Click "Continue to Dashboard"
3. Try these actions:
   - Navigate to **Ideas** page
   - Click "Submit New Idea"
   - Fill in the form and submit
   - Click on any idea card to view details
   - Try liking an idea
   - Try volunteering for an idea
   - Add a comment
   - Navigate to **Profile** to see your submissions

### Testing as Faculty
1. Switch role by clicking "Switch Role" in the top bar
2. Select **Faculty** on the login page
3. Try these actions:
   - Navigate to **Ideas** page
   - Click on an unendorsed idea
   - Click the "✅ Endorse" button
   - Add a comment (note: faculty comments appear first)
   - Navigate to **Profile** to see your endorsed items

### Testing as Administrator
1. Switch role again
2. Select **Administrator** on the login page
3. Try these actions:
   - Navigate to **Events** page
   - Click on any event
   - Click "🔄 Change Status" to update event status
   - Try the "🗑️ Delete" button (be careful!)
   - Navigate to **Profile** to see platform statistics

## 📝 Sample Data

The application comes pre-loaded with sample data:

### Ideas
- **Campus Sustainability Initiative** (45 likes, 12 volunteers, endorsed)
- **Peer Tutoring Platform** (38 likes, 8 volunteers)
- **Mental Health Awareness Week** (67 likes, 15 volunteers, endorsed)

### Events
- **Annual Science Fair** (89 likes, Approved, Faculty Chosen)
- **Community Service Day** (52 likes, Under Review)
- **Cultural Festival** (103 likes, Planned, endorsed)

## 🎯 Key Features to Test

### Dashboard
- View most upvoted item
- View most volunteered idea
- View faculty chosen event
- See recent ideas and events

### Ideas Page
- Filter by course
- Submit new ideas (students)
- Like and volunteer for ideas
- View detailed idea information
- Read and post comments
- Endorse ideas (faculty)
- Delete inappropriate ideas (admin)

### Events Page
- Filter by status
- Suggest new events
- Like events
- View detailed event information
- Change event status (admin)
- Delete events (admin)

### Profile Page
- View role-specific information
- See statistics
- Track submissions (students)
- View endorsements (faculty)
- See platform overview (admin)

## 💡 Tips

1. **Switching Roles**: Use the "Switch Role" button in the top navigation bar to test different user perspectives

2. **Role Capabilities**:
   - Students see submission forms and can volunteer
   - Faculty see endorsement buttons
   - Admins see delete and status management buttons

3. **Comments**: Faculty comments are automatically highlighted in blue and appear at the top

4. **Navigation**: Use the pill-shaped navigation bar to move between sections:
   - 🏠 Dashboard
   - 💡 Ideas
   - 📅 Events
   - 👤 Profile

5. **Data Persistence**: Currently using in-memory storage, so data resets when you restart the application

## 🔍 Testing Checklist

### Student Actions
- [ ] Submit a new idea
- [ ] Submit a new event suggestion
- [ ] Like an idea
- [ ] Volunteer for an idea
- [ ] Add a comment
- [ ] View profile statistics
- [ ] Filter ideas by course
- [ ] Filter events by status

### Faculty Actions
- [ ] Endorse an idea
- [ ] Add a prioritized comment
- [ ] View endorsed ideas in profile
- [ ] Review student submissions

### Admin Actions
- [ ] Change event status
- [ ] Delete an inappropriate idea
- [ ] Delete an event
- [ ] View platform statistics
- [ ] Review pending items

## ❓ Common Issues

### Issue: Application won't start
**Solution**: Ensure you have .NET 8.0 SDK installed. Check with `dotnet --version`

### Issue: Port already in use
**Solution**: The application will automatically try alternative ports. Check the console output for the actual URL.

### Issue: Role not persisting
**Solution**: Clear browser localStorage and select role again on the login page.

### Issue: Can't see role-specific buttons
**Solution**: Make sure you've selected a role on the login page. Check localStorage in browser dev tools.

## 🎓 Learning Paths

### For Developers
1. Explore `Controllers/ApiController.cs` for REST API endpoints
2. Check `Services/InMemoryDataService.cs` for data management
3. Review `Models/` for entity structures
4. Examine `Views/` for UI implementation

### For Designers
1. Review `Views/Home/*.cshtml` for page layouts
2. Check `wwwroot/css/site.css` for styling
3. Note the consistent color scheme and gradient usage

### For Product Managers
1. Test all user flows
2. Review the About page for mission alignment
3. Check SDG integration
4. Evaluate user experience across roles

## 📚 Next Steps

After exploring the application:
1. Read the full `README.md` for detailed documentation
2. Check the API documentation section
3. Review the Technology Stack
4. Explore future enhancement ideas
5. Consider contributing!

## 🤝 Need Help?

- Check the main README.md
- Review code comments
- Contact: taskmates@university.edu

---

**Happy Testing!** 🎉

Remember: This is a demonstration platform. Feel free to experiment and break things - that's how we learn!

