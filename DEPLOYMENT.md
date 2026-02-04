# 🚀 ERP System - Deployment Guide

## 🎭 Sandbox Mode (Demo Mode)

This application runs in **Sandbox Mode** by default:
- **Every visitor gets their own isolated database**
- **All changes are temporary** and visible only to that visitor
- **Sessions expire after 20 minutes** (auto-reset to original state)
- **No authentication required** - perfect for portfolio demos

---

## 📦 Deployment Options (Free Tier)

### Option 1: **Render.com** (Recommended - Easiest)

1. **Push to GitHub**
   ```bash
   git add .
   git commit -m "Add sandbox mode and Docker deployment"
   git push origin main
   ```

2. **Deploy on Render**
   - Go to [render.com](https://render.com) and sign up
   - Click **"New" → "Web Service"**
   - Connect your GitHub repository
   - Render will auto-detect the `render.yaml` file
   - Click **"Create Web Service"**
   - Wait 5-10 minutes for the first deploy

3. **Your app will be live at**: `https://erp-system-demo.onrender.com`

> ⚠️ **Note**: Free tier spins down after 15 min of inactivity. First request may take 30-60 seconds.

---

### Option 2: **Railway.app**

1. Go to [railway.app](https://railway.app)
2. Click **"Deploy from GitHub"**
3. Select your repository
4. Railway will auto-detect the Dockerfile
5. Add environment variable: `PORT=8080`
6. Deploy!

---

### Option 3: **Fly.io**

1. Install Fly CLI: 
   ```bash
   # Windows (PowerShell as Admin)
   iwr https://fly.io/install.ps1 -useb | iex
   ```

2. Login and deploy:
   ```bash
   fly auth login
   fly launch --name erp-demo
   fly deploy
   ```

---

## 🏗️ Project Structure (Sandbox Architecture)

```
Program.cs
├── SandboxDbContextFactory (Singleton)
│   └── Manages all session databases
├── SandboxDbContextProvider (Scoped)
│   └── Resolves correct DB for current request
└── SandboxSessionMiddleware
    └── Creates/tracks session cookies
```

### How It Works:
1. Visitor opens the site → Gets a unique session cookie
2. First request → Creates an **in-memory SQLite database** for that session
3. Database is seeded with sample data (employees, equipment, etc.)
4. All changes are **isolated** to that session's database
5. After 20 minutes → Session expires, database is destroyed
6. Next visit → Fresh database with original sample data

---

## 🛠️ Local Development

```bash
# Navigate to project
cd ERP

# Run the application
dotnet run --project ERP.csproj

# Open in browser
# http://localhost:5254
```

---

## 🐳 Docker Local Test

```bash
# Build the image
docker build -t erp-demo .

# Run the container
docker run -p 8080:8080 -e PORT=8080 erp-demo

# Open http://localhost:8080
```

---

## 📊 Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `PORT` | Server port | `8080` |
| `ASPNETCORE_ENVIRONMENT` | Environment mode | `Production` |

---

## ⚡ Performance Notes

- Each session uses ~5-10MB of memory
- Cleanup runs every 2 minutes (removes expired sessions)
- In-memory SQLite is very fast for read/write operations
- Suitable for ~50 concurrent demo users on free tier

---

## 🔧 Customization

### Change Session Duration (default: 20 minutes)

Edit `Program.cs`:
```csharp
builder.Services.AddSingleton<SandboxDbContextFactory>(sp => 
    new SandboxDbContextFactory(sp, TimeSpan.FromMinutes(30))); // Change to 30 min
```

### Add More Seed Data

Edit `Data/RuntimeDataSeeder.cs` to add more sample employees, equipment, etc.

---

## 📝 Tech Stack

- **Backend**: ASP.NET Core 9.0 (MVC)
- **Database**: SQLite (In-Memory per session)
- **ORM**: Entity Framework Core 9.0
- **Frontend**: Razor Views + Bootstrap 5
- **Deployment**: Docker

---

## 🎓 For Your Portfolio

Share this link format:
```
https://your-app-name.onrender.com
```

Recruiter/Viewer experience:
1. Click link → Page loads (30-60 sec on first load)
2. See demo banner with countdown timer
3. Can modify data freely (add employees, assign equipment, etc.)
4. Their changes don't affect other visitors
5. After 20 min → Fresh demo state

**No login. No mess. Clean demo every time.** ✨
