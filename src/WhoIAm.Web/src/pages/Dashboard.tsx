import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuthStore } from '../store/authStore';

const Dashboard: React.FC = () => {
  const navigate = useNavigate();
  const { username, logout } = useAuthStore();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-gray-900 to-black">
      <nav className="bg-gray-800 border-b border-gray-700">
        <div className="container flex justify-between items-center py-4">
          <h1 className="text-2xl font-bold text-white">Who I Am</h1>
          <div className="flex items-center gap-4">
            <span className="text-gray-300">Welcome, {username}</span>
            <button
              onClick={handleLogout}
              className="btn-secondary"
            >
              Logout
            </button>
          </div>
        </div>
      </nav>

      <main className="container py-8">
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          <div className="card">
            <h3 className="text-xl font-bold text-white mb-4">Create Your Identity</h3>
            <p className="text-gray-300 mb-4">Build your virtual persona and express your authentic self.</p>
            <button
              onClick={() => navigate('/create-identity')}
              className="btn-primary w-full"
            >
              Create Identity
            </button>
          </div>

          <div className="card">
            <h3 className="text-xl font-bold text-white mb-4">Dark Room</h3>
            <p className="text-gray-300 mb-4">Write your thoughts in a private, safe space.</p>
            <button
              onClick={() => navigate('/journal')}
              className="btn-primary w-full"
            >
              Open Journal
            </button>
          </div>

          <div className="card">
            <h3 className="text-xl font-bold text-white mb-4">Create Your Enemy</h3>
            <p className="text-gray-300 mb-4">Face your symbolic challenges and overcome them.</p>
            <button
              onClick={() => navigate('/create-enemy')}
              className="btn-primary w-full"
            >
              Create Enemy
            </button>
          </div>

          <div className="card">
            <h3 className="text-xl font-bold text-white mb-4">Communities</h3>
            <p className="text-gray-300 mb-4">Join communities and connect with like-minded people.</p>
            <button
              onClick={() => navigate('/communities')}
              className="btn-primary w-full"
            >
              Explore Communities
            </button>
          </div>

          <div className="card">
            <h3 className="text-xl font-bold text-white mb-4">Real-Life Missions</h3>
            <p className="text-gray-300 mb-4">Accept challenges and improve your real life.</p>
            <button
              onClick={() => navigate('/missions')}
              className="btn-primary w-full"
            >
              View Missions
            </button>
          </div>

          <div className="card">
            <h3 className="text-xl font-bold text-white mb-4">Social Feed</h3>
            <p className="text-gray-300 mb-4">See what others are sharing and express yourself.</p>
            <button
              onClick={() => navigate('/feed')}
              className="btn-primary w-full"
            >
              View Feed
            </button>
          </div>
        </div>
      </main>
    </div>
  );
};

export default Dashboard;
