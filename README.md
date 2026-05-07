# RERIS: Robotics Interface System

## Overview
RERIS (Robotics Interface System) is a comprehensive web-based GUI platform that bridges the complexity of ROS (Robot Operating System) with an intuitive browser interface. Built with Vue.js and powered by a .NET Core backend, RERIS transforms advanced robotics control into accessible web-based interactions.

**Key Innovation:** RERIS eliminates the need for technical expertise in ROS commands, replacing complex terminal operations and RViz with point-and-click web controls accessible from anywhere.

---

## 🗺️ Mapping System Features

### **SLAM Real-Time Mapping**
- **Live Map Creation:** Autonomous map generation using LiDAR SLAM while robot explores
- **Real-Time Visualization:** Watch maps build dynamically as occupancy grids during exploration  
- **Multiple Map Support:** Save, load, and manage multiple map files per location
- **Map Export/Import:** Export maps as .pgm and .yaml files for ROS compatibility

![create_map_new](https://github.com/user-attachments/assets/47780e12-e072-4e6f-b028-d60b61020623)

### **Interactive Map Editor**
- **Mouse-Based Editing:** Click and drag to remove unwanted obstacles or add navigable areas
- **Occupancy Grid Display:** Visual representation using grid squares (occupied/free/unknown)
- **Map Visualization Controls:** Zoom in/out, pan, and navigate large map areas
- **Automatic Saving:** Maps automatically saved to MongoDB database

---

## 🎮 Robot Control System

### **Click-to-Navigate Interface**
- **Point-and-Click Navigation:** Simply click any point on the map to send robot there
- **Real-Time Path Planning:** Automatic route calculation with obstacle avoidance
- **Live Progress Tracking:** Watch robot movement and pathfinding in real-time
- **Goal Management:** Cancel current goals or set new targets during navigation

![reaching_the_target](https://github.com/user-attachments/assets/7622fb21-63f2-4515-a260-0283439f36d7)

### **Advanced Control Features**
- **Emergency Stop:** Instant robot halt via web interface
- **Manual Velocity Control:** Direct speed and direction control when needed
- **Obstacle Avoidance:** Automatic detection and avoidance of static and dynamic obstacles
- **Safety Systems:** Automatic stop within 154ms if connection is lost

---

## 📹 Sensor Data Visualization

### **Live Camera Streaming**
- **Real-Time Video:** Live feed from Intel RealSense D415 camera
- **Depth Information:** RGB + Depth data visualization
- **Multiple View Modes:** Switch between color, depth, and combined views

### **LiDAR Point Cloud Display**
- **3D Point Cloud Viewer:** Real-time 3D visualization of LiDAR scan data
- **360° Coverage:** Complete environmental sensing from RPLidar C1
- **Interactive Viewing:** Rotate and zoom 3D point cloud data

### **Robot Status Monitoring**
- **Position Tracking:** Real-time robot position display on map
- **Sensor Readings:** Live IMU, odometry, and battery status
- **Connection Status:** Visual indicators for robot communication health
- **Performance Metrics:** Response time and system performance monitoring

---

## 👥 User Management System

### **Multi-User Support**
- **Concurrent Users:** Support up to 10 simultaneous users
- **Session Management:** Individual user sessions with isolated controls
- **Collaborative Features:** Multiple operators can monitor same robot

### **Role-Based Access Control**
- **Admin Privileges:** Full system access including map creation and user management
- **User Permissions:** Navigation and monitoring access with restricted admin functions
- **Authentication:** JWT token-based login system
- **Permission Control:** Granular control over mapping and navigation permissions

![admin_registration](https://github.com/user-attachments/assets/8b27fcc2-5211-4426-b023-d9ca06b134e7)

---

## 🏢 Site Management

### **Multi-Location Support**
- **Site Organization:** Manage multiple locations (Floor 1, Warehouse A, Lab B, etc.)
- **Site Switching:** Instantly switch between different operational areas
- **Site-Specific Maps:** Each location maintains its own map and configuration
- **Configuration Management:** Individual parameter settings per site

---

## 🌐 Communication Architecture

### **WebRTC P2P Protocol**
- **Direct Communication:** Peer-to-peer connection between browser and robot
- **Low Latency:** Average response time of 14.1ms under optimal conditions
- **Three-Channel System:**
  - **Telemetry Channel (Port 8440):** Robot commands, status, and control data
  - **Map Channel (Port 8441):** Map data streaming and updates  
  - **Media Channel (Port 8442):** Camera feeds and point cloud data

### **Advanced Communication Features**
- **Binary Protocol:** Custom binary frame protocol reduces bandwidth usage
- **Fail-Safe Mode:** Automatic robot shutdown if connection lost within 154ms
- **Network Adaptation:** Automatic adjustment to network conditions (14.1ms to 100.2ms response time range)

---

## 🚀 Performance Specifications

### **System Performance**
- **Response Time:** 14.1ms average (low latency) / 100.2ms (high latency conditions)
- **Navigation Accuracy:** ±0.027 meters in real-world environments  
- **Multi-User Efficiency:** 10 concurrent users with only 53% CPU usage
- **Mapping Speed:** Average map creation time of 5.1 seconds per task
- **Obstacle Avoidance:** 100% success rate in both simulation and real environments

### **Safety & Reliability**
- **Emergency Response:** Robot stops within 154.1ms during signal loss
- **Dynamic Obstacle Handling:** Real-time detection and avoidance of moving objects
- **System Stability:** Continuous operation with automated error recovery

---

## 🛠️ Tech Stack

### **Frontend:**
- **Vue.js 3 with Quasar Framework:** Reactive UI framework for responsive design
- **Vuex:** Centralized state management for real-time data updates
- **Vue Router:** SPA navigation between application pages
- **WebRTC:** Direct peer-to-peer communication with robot systems
- **Axios:** HTTP client for REST API communication with backend

### **Backend:**
- **.NET Core 6:** High-performance backend API framework
- **MongoDB:** NoSQL database for map data and user management
- **JWT Authentication:** Secure token-based user authentication
- **WebRTC Signaling Server:** Connection establishment and management
- **Binary Protocol:** Custom protocol for optimized data transmission

### **Robotics Stack:**
- **ROS 2 Humble:** Robot Operating System for hardware abstraction
- **SLAM Toolbox:** Real-time mapping and localization
- **Nav2 Stack:** Advanced navigation and path planning
- **WebRTC Bridge:** Custom ROS 2 package for web communication
- **Intel RealSense SDK:** Camera integration and depth processing
- **RPLidar SDK:** LiDAR sensor integration

---

## 📁 Project Architecture

```
RERIS System/
├── frontend/ (Vue.js 3)
│   ├── src/
│   │   ├── components/     # Reusable Vue components
│   │   │   ├── MapView/    # Interactive map display
│   │   │   ├── Controls/   # Robot control panels  
│   │   │   ├── Camera/     # Live video streaming
│   │   │   └── Dashboard/  # Main interface dashboard
│   │   ├── views/          # Application pages
│   │   │   ├── Login.vue   # User authentication
│   │   │   ├── Mapping.vue # Map creation interface
│   │   │   └── Control.vue # Robot operation page
│   │   ├── store/          # Vuex state management
│   │   │   ├── robot.js    # Robot state and control
│   │   │   ├── mapping.js  # Map data management
│   │   │   └── user.js     # User session management
│   │   └── services/       # API and WebRTC services
│   └── public/             # Static assets and configuration
│
├── backend/ (.NET Core)
│   ├── Controllers/        # REST API endpoints
│   │   ├── AuthController  # User authentication
│   │   ├── MapController   # Map management
│   │   └── RobotController # Robot communication
│   ├── Services/           # Business logic services
│   │   ├── WebRTCService   # P2P connection management
│   │   ├── UserService     # User management
│   │   └── MapService      # Map data processing
│   ├── Models/             # Data models and DTOs
│   └── Database/           # MongoDB integration
│
├── ros2_package/ (Python)
│   ├── reris_webctl/       # Main ROS 2 package
│   │   ├── webrtc_bridge   # WebRTC communication node
│   │   ├── map_streamer    # Map data streaming
│   │   └── control_node    # Robot command interface
│   └── launch/             # ROS 2 launch files
│
└── robot_config/
    ├── urdf/               # Robot description files
    ├── navigation/         # Nav2 configuration
    └── sensors/            # Sensor parameter files
```

---

## 🚀 Getting Started

### **Prerequisites**
- **Frontend:** Node.js 16+, npm/yarn
- **Backend:** .NET 6 SDK, MongoDB 4.4+
- **Robot:** ROS 2 Humble, Ubuntu 22.04
- **Hardware:** Intel RealSense D415, RPLidar C1

### **Installation & Setup**
Detailed installation guides and configuration instructions will be provided in separate documentation files for each system component.

---

## 📈 Development Status

**Current Version:** Beta 1.0  
**Status:** Production Ready  
**Testing:** Complete system integration testing completed  
**Documentation:** Comprehensive user and technical documentation available

### **Upcoming Features**
- Mobile app companion (iOS/Android)
- Multi-robot fleet management
- Advanced analytics dashboard
- Cloud deployment options
- Machine learning integration for path optimization

---

## 📄 License & Contribution

This project is part of academic research at King Mongkut's University of Technology North Bangkok (KMUTNB). Please refer to the license file for usage terms and contribution guidelines.

---

## 🤝 Support & Contact

For technical support, feature requests, or collaboration inquiries, please contact the development team through the university's official channels.

**Research Team:**  
Department of Mechatronics Engineering  
King Mongkut's University of Technology North Bangkok
