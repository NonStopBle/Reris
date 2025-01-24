# Reris: Robotics Interface System

## Overview
Reris (Robotics Interface System) is a web-based application built with Vue.js that provides an intuitive interface for:
- Recreating a robot's map using LiDAR scanning.
- Setting the robot's navigation targets.

This project enables seamless interaction with robotics systems, leveraging modern front-end development techniques to provide a responsive and user-friendly experience.

---

## Features
- **Real-Time LiDAR Mapping:**
  - Visualize the robot's environment in real time with LiDAR data.
  - Dynamically update the map as new data is received.

- **Target Selection:**
  - Click to set navigation targets for the robot.
  - Display the robot’s pathfinding progress and status updates.

- **Responsive UI:**
  - Optimized for desktop and mobile devices.
  - Clean and intuitive interface for robotics management.

---

## Tech Stack
### Frontend:
- **Vue.js 3**: Reactive framework for building the application.
- **Vuex**: State management for real-time data updates.
- **Vue Router**: Navigation between app pages.
- **Websocket**: For communicate with robotics and ros system.
- **Axios**: Handling API requests to the backend.

### Backend:
- (Optional) Any backend API service (e.g., Node.js, Flask, or similar) for processing and streaming LiDAR data, robot status, and navigation commands.

---

## Project Structure
```
├── src
│   ├── assets       # Static assets (images, icons, etc.)
│   ├── components   # Vue components (e.g., MapView, TargetSelector)
│   ├── store        # Vuex store modules
│   ├── views        # Application views/pages
│   ├── router       # Vue Router configuration
│   ├── utils        # Utility functions (e.g., data parsing, formatting)
│   └── App.vue      # Root Vue component
├── public           # Static files
├── package.json     # Project dependencies
├── README.md        # Project documentation
└── .env             # Environment variables
```

---

## Project in development 

---

