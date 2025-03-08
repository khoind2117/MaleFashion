import React, { useEffect, useState } from 'react';
import MainLayout from '../../layouts/MainLayout';
import { toast } from 'react-toastify';
import { useAuth } from '../../context/AuthContext';
import { useNavigate } from 'react-router';

const Register = () => {
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [email, setEmail] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [address, setAddress] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [error, setError] = useState<string | null>(null);

  const { register, isAuthenticated } = useAuth();
  const navigate = useNavigate();
  
  useEffect(() => {
    if (isAuthenticated()) {
      navigate('/cart');
    }
  }, [isAuthenticated, navigate]);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    if (password !== confirmPassword) {
      setError('Mật khẩu xác nhận không khớp');
      return;
    }

    try {
      await register({
        firstName,
        lastName,
        email,
        phoneNumber,
        address,
        password,
      });
    } catch (error) {
      console.error(error);
      setError('Đăng ký thất bại');
      toast.error("Đăng ký thất bại")
    }
  };

  return (
    <MainLayout>
      <div className="container">
        <form onSubmit={handleSubmit} role="form">
          <h1>Đăng ký</h1>

          {/* Error Message */}
          {error && <div className="text-danger">{error}</div>}

          <div className="row">
            <div className="form-group mb-2 col-6">
              <label htmlFor="LastName">Họ của bạn</label>
              <input
                id="LastName"
                type="text"
                className="form-control"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
              />
            </div>
            <div className="form-group mb-2 col-6">
              <label htmlFor="FirstName">Tên của bạn</label>
              <input
                id="FirstName"
                type="text"
                className="form-control"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
              />
            </div>
          </div>

          <div className="row">
            <div className="form-group mb-2 col-6">
              <label htmlFor="PhoneNumber">Số điện thoại</label>
              <input
                id="PhoneNumber"
                type="text"
                className="form-control"
                value={phoneNumber}
                onChange={(e) => setPhoneNumber(e.target.value)}
              />
            </div>
            <div className="form-group mb-2 col-6">
              <label htmlFor="Email">Tài khoản Email</label>
              <input
                id="Email"
                type="email"
                className="form-control"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
              />
            </div>
          </div>

          <div className="row">
            <div className="form-group mb-2 col-6">
              <label htmlFor="Password">Mật khẩu</label>
              <input
                id="Password"
                type="password"
                className="form-control"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
              />
            </div>
            <div className="form-group mb-2 col-6">
              <label htmlFor="ConfirmPassword">Mật khẩu xác nhận</label>
              <input
                id="ConfirmPassword"
                type="password"
                className="form-control"
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
              />
            </div>
          </div>

          <div className="form-group mb-2">
            <label htmlFor="Address">Địa chỉ của bạn</label>
            <input
              id="Address"
              type="text"
              className="form-control"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
            />
          </div>

          <button type="submit" className="btn btn-primary w-100">
            Đăng ký
          </button>
        </form>
      </div>
    </MainLayout>
  );
};

export default Register;
