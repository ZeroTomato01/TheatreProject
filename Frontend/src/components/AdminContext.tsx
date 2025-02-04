import React, { createContext, useState, useEffect, ReactNode } from 'react';

interface AdminContextProps {
  isLoggedIn: boolean;
  setIsLoggedIn: (loggedIn: boolean) => void;
}

const AdminContext = createContext<AdminContextProps | undefined>(undefined);

export const AdminProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(() => {
    const savedLoggedIn = localStorage.getItem('isLoggedIn');
    return savedLoggedIn ? JSON.parse(savedLoggedIn) : false;
  });

  useEffect(() => {
    localStorage.setItem('isLoggedIn', JSON.stringify(isLoggedIn));
  }, [isLoggedIn]);

  return (
    <AdminContext.Provider value={{ isLoggedIn, setIsLoggedIn }}>
      {children}
    </AdminContext.Provider>
  );
};

export const useAdmin = () => {
  const context = React.useContext(AdminContext);
  if (!context) {
    throw new Error('useAdmin must be used within an AdminProvider');
  }
  return context;
};