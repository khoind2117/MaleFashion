import React, { useState } from 'react';
import MainLayout from '../../layouts/MainLayout';
import { useNavigate } from 'react-router-dom';
import { AuthContext } from '../../context/AuthContext';
import { toast } from 'react-toastify';

const Login = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState<string | null>(null);

  const { login } = React.useContext(AuthContext)!;
  const navigate = useNavigate();

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    try {
      await login({ userName: email, password });
      toast.success("Đăng nhập thành công")

      navigate('/');
    } catch (error) {
      console.error(error);
      setError('Đăng nhập thất bại');
      toast.error("Đăng nhập thất bại")

    }
  };

  return (
    <MainLayout>
      <div className="container">      
        <form onSubmit={handleSubmit} role="form">
          <h1>Đăng nhập</h1>
          <hr />

          {/* Error Message */}
          {error && <div className="text-danger">{error}</div>}

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
