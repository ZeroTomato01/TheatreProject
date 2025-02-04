import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAdmin } from './AdminContext';

const Logout: React.FC = () => {
  const { setIsLoggedIn } = useAdmin();
  const navigate = useNavigate();

  useEffect(() => {
    setIsLoggedIn(false);
    navigate('/Login');
  }, [setIsLoggedIn, navigate]);

  return null;
};

export default Logout;