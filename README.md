# TaskMates Application

![TaskMates Logo](https://via.placeholder.com/800x200?text=TaskMates+-+Empowering+Ideas+%26+Events)

## 🎯 Overview

TaskMates is a comprehensive web application designed to foster collaboration and innovation within school communities. It provides a platform where students can share creative ideas, propose events, volunteer for projects, and receive support from faculty and administrators.

## ✨ Features

### 🔐 Role-Based Access Control

The application supports three distinct user roles, each with specific capabilities:

#### **Students** 🎓
- Submit creative ideas with course association
- Suggest events for school-wide activities
- Upvote ideas and events they support
- Volunteer for ideas they're passionate about
- Comment on ideas and events
- View personal submission and volunteering history

#### **Faculty** 👨‍🏫
- Endorse promising student ideas
- Provide prioritized feedback (faculty comments appear first)
- Review and support student initiatives
- Track endorsed ideas and events

#### **Administrators** ⚙️
- Delete inappropriate content
- Change event status (Under Review, Approved, Planned, Rejected)
- Mark events as "Faculty Chosen" schoolwide events
- Moderate comments and discussions
- View platform-wide statistics and analytics

### 📱 Key Pages

#### 1. **Introduction/Login Page**
- Beautiful landing page explaining TaskMates
- Role selection interface (Student, Faculty, Administrator)
- Overview of platform capabilities

#### 2. **Dashboard**
- Displays most upvoted idea or event
- Shows most volunteered idea
- Highlights faculty chosen schoolwide event
- Recent activity overview
- Easy navigation to all platform sections

#### 3. **Ideas Page**
- Scrollable list of all submitted ideas
- Each idea card shows:
  - Title and description
  - Author and creation date
  - Number of likes and volunteers
  - Endorsement status
  - Course association
- Click to view detailed idea information
- Comment section with faculty priority
- Filter by course
- Submission form for new ideas (students)
- Faculty endorsement controls
- Admin deletion controls

#### 4. **Events Page**
- Scrollable list of all proposed events
- Each event card shows:
  - Title and description
  - Author and dates
  - Number of likes
  - Event status badge
  - Endorsement information
- Click to view detailed event information
- Comment section with faculty priority
- Filter by status
- Submission form for new events
- Admin status management
- Admin deletion controls

#### 5. **Profile Page**
Role-specific profile information:

**Student Profiles:**
- Personal information
- All submitted ideas
- All suggested events
- Volunteering history
- Statistics (total likes received, etc.)

**Faculty Profiles:**
- Personal information
- Endorsed ideas
- Endorsed events
- Total endorsements count

**Admin Profiles:**
- Personal information
- Platform statistics
- Total events and ideas managed
- Pending review items
- Platform overview

#### 6. **About Us Page**
- Platform purpose and mission
- Objectives and goals
- Sustainable Development Goals (SDG) alignment
  - SDG 4: Quality Education
  - SDG 10: Reduced Inequalities
  - SDG 17: Partnerships for the Goals
- Team information with social media links
- Contact information

## 🛠️ Technology Stack

### Backend
- **ASP.NET Core 8.0** - Web framework
- **C#** - Programming language
- **Entity Framework Core** - ORM (configured for SQL Server)
- **Identity Framework** - Authentication system

### Frontend
- **Razor Pages** - Server-side rendering
- **Bootstrap 5** - UI framework
- **JavaScript (ES6+)** - Client-side interactivity
- **Fetch API** - REST API communication
- **LocalStorage** - User session management

### Architecture
- **MVC Pattern** - Model-View-Controller architecture
- **RESTful API** - Backend API endpoints
- **In-Memory Data Service** - Data persistence (easily swappable with database)
- **Dependency Injection** - Service management

## 📦 Project Structure

```
TaskMatesApplication/
├── TaskMates/
│   ├── Controllers/
│   │   ├── HomeController.cs          # Main page controller
│   │   └── ApiController.cs           # REST API endpoints
│   ├── Models/
│   │   ├── Idea.cs                    # Idea entity model
│   │   ├── Event.cs                   # Event entity model
│   │   ├── Comment.cs                 # Comment entity model
│   │   └── UserProfile.cs             # User profile model
│   ├── Services/
│   │   ├── IDataService.cs            # Data service interface
│   │   └── InMemoryDataService.cs     # In-memory implementation
│   ├── Data/
│   │   └── ApplicationDbContext.cs    # Database context
│   ├── Views/
│   │   ├── Home/
│   │   │   ├── Index.cshtml           # Dashboard
│   │   │   ├── Ideas.cshtml           # Ideas page
│   │   │   ├── Events.cshtml          # Events page
│   │   │   ├── Profile.cshtml         # Profile page
│   │   │   ├── About.cshtml           # About Us page
│   │   │   └── PseudoLogin.cshtml     # Login/Role selection
│   │   └── Shared/
│   │       └── _Layout.cshtml         # Main layout with navigation
│   ├── wwwroot/
│   │   ├── css/
│   │   │   └── site.css               # Custom styles
│   │   └── js/
│   │       └── site.js                # Custom JavaScript
│   └── Program.cs                     # Application entry point
└── README.md                          # This file
```

## 🚀 Getting Started

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- SQL Server (optional - using in-memory data service by default)

### Installation

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd TaskMatesApplication
   ```

2. **Restore dependencies**
   ```bash
   cd TaskMates
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Open in browser**
   Navigate to `https://localhost:5001` or `http://localhost:5000`

### First Time Setup

1. On first launch, you'll see the **Introduction/Login page**
2. Select your role (Student, Faculty, or Administrator)
3. You'll be redirected to the Dashboard
4. Explore the platform!

**Demo Users:**
- Student: Alex Johnson (alex.johnson@university.edu)
- Faculty: Dr. Sarah Williams (sarah.williams@university.edu)
- Administrator: Michael Chen (michael.chen@university.edu)

## 📖 API Documentation

### Ideas Endpoints

```
GET    /api/ideas              - Get all ideas
GET    /api/ideas/{id}         - Get idea by ID with comments
POST   /api/ideas              - Create new idea
POST   /api/ideas/{id}/like    - Like/unlike an idea
POST   /api/ideas/{id}/volunteer - Volunteer for an idea
POST   /api/ideas/{id}/endorse - Endorse an idea (faculty only)
DELETE /api/ideas/{id}         - Delete an idea (admin only)
```

### Events Endpoints

```
GET    /api/events             - Get all events
GET    /api/events/{id}        - Get event by ID with comments
POST   /api/events             - Create new event
POST   /api/events/{id}/like   - Like/unlike an event
POST   /api/events/{id}/status - Update event status (admin only)
DELETE /api/events/{id}        - Delete an event (admin only)
```

### Comments Endpoints

```
GET    /api/comments/{itemId}  - Get comments for an item
POST   /api/comments           - Create new comment
```

### Profile Endpoints

```
GET    /api/profile/{userName} - Get user profile
```

## 🎨 Design Features

- **Modern Gradient UI** - Beautiful color schemes for different sections
- **Responsive Design** - Works perfectly on desktop, tablet, and mobile
- **Smooth Animations** - Card hover effects and transitions
- **Intuitive Navigation** - Pill-shaped navigation bar
- **Status Badges** - Visual indicators for event status and endorsements
- **Role-Based UI** - Interface adapts based on user role
- **Faculty-Priority Comments** - Faculty comments are highlighted and sorted first
- **Real-time Updates** - Dynamic content loading without page refresh

## 🌍 Sustainable Development Goals

TaskMates actively contributes to three UN Sustainable Development Goals:

### SDG 4: Quality Education
Enhances learning through peer collaboration, idea sharing, and active participation in campus initiatives.

### SDG 10: Reduced Inequalities
Provides equal voice to all students regardless of background, with merit-based idea promotion.

### SDG 17: Partnerships for the Goals
Strengthens partnerships between students, faculty, and administrators for a cohesive educational environment.

## 🔒 Security Features

- Role-based access control
- Input sanitization and validation
- XSS protection through HTML escaping
- CSRF protection (built into ASP.NET Core)
- Safe localStorage usage for session management

## 🧪 Testing

Currently using in-memory data service with seeded sample data. The application includes:
- 3 sample ideas with varying likes and volunteers
- 3 sample events with different statuses
- Sample comments demonstrating faculty priority
- Mock user profiles for each role

## 📈 Future Enhancements

- [ ] Email notifications for endorsements and event status changes
- [ ] Image upload for ideas
- [ ] Real-time chat for volunteers
- [ ] Analytics dashboard for admins
- [ ] Export reports (PDF/Excel)
- [ ] Mobile app (React Native)
- [ ] Integration with school calendar
- [ ] Voting deadlines for ideas
- [ ] Achievement badges for active participants

## 🤝 Contributing

We welcome contributions! Please feel free to:
1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👥 Team

- **Alex Johnson** - Lead Developer
- **Maria Garcia** - Backend Engineer
- **David Chen** - UI/UX Designer
- **Sarah Williams** - Product Manager

## 📧 Contact

- Email: taskmates@university.edu
- Phone: +1 (555) 123-4567
- Location: University Campus, Building A

---

**TaskMates** - Empowering Ideas, Enabling Events, Building Community 🎓

© 2025 TaskMates. All rights reserved.

