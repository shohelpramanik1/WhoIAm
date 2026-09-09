import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { useAuthStore } from './store/authStore';
import Login from './pages/Login';
import Register from './pages/Register';
import Dashboard from './pages/Dashboard';
import './index.css';

const ProtectedRoute: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const accessToken = useAuthStore((state) => state.accessToken);
  return accessToken ? <>{children}</> : <Navigate to="/login" />;
};

function App() {
  const accessToken = useAuthStore((state) => state.accessToken);

  return (
    <Router>
      <Routes>
        <Route path="/login" element={accessToken ? <Navigate to="/dashboard" /> : <Login />} />
        <Route path="/register" element={accessToken ? <Navigate to="/dashboard" /> : <Register />} />
        <Route
          path="/dashboard"
          element={
            <ProtectedRoute>
              <Dashboard />
            </ProtectedRoute>
          }
        />
        <Route path="/" element={<Navigate to={accessToken ? "/dashboard" : "/login"} />} />
      </Routes>
    </Router>
  );
}

export default App;
