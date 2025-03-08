import React, { useEffect, useState } from 'react';
import MainLayout from '../../layouts/MainLayout';
import { useAuth } from '../../context/AuthContext';
import { useNavigate } from 'react-router';

const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const { login, isAuthenticated } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    if (isAuthenticated()) {
      navigate('/cart');
    }
  }, [isAuthenticated, navigate]);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    try {
      await login({ userName: email, password });
    } catch (error) {
      console.error("Error during login attempt:", error);
    }
  };

  return (
    <MainLayout>
      <div className="container">      
        <form onSubmit={handleSubmit} role="form">
          <h1>Đăng nhập</h1>
          <hr />

          <div className="form-group mb-3">
            <label htmlFor="Email">Email</label>
            <input
              id="Email"
              type="email"
              className="form-control"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>

          <div className="form-group mb-3">
            <label htmlFor="Password">Mật khẩu</label>
            <input
              id="Password"
              type="password"
              className="form-control"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>

          <div className="form-group">
            <button type="submit" className="btn btn-primary w-100">
              Đăng nhập
            </button>
          </div>

          <p>
            Bạn chưa có tài khoản?{' '}
            <a href="/register">Tạo tài khoản ở đây</a>
          </p>
        </form>
      </div>
    </MainLayout>
  );
};

export default Login;
