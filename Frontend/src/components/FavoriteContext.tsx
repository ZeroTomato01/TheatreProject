import React, { createContext, useState, useEffect, ReactNode } from 'react';

interface FavoriteItem {
  showDateId: number;
  showTitle: string;
  showDate: string;
  showPrice: number;
}

interface FavoriteContextProps {
  favorites: FavoriteItem[];
  addToFavorites: (item: FavoriteItem) => void;
  removeFromFavorites: (showDateId: number) => void;
}

const FavoriteContext = createContext<FavoriteContextProps | undefined>(undefined);

export const FavoriteProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [favorites, setFavorites] = useState<FavoriteItem[]>(() => {
    const savedFavorites = localStorage.getItem('favorites');
    return savedFavorites ? JSON.parse(savedFavorites) : [];
  });

  useEffect(() => {
    localStorage.setItem('favorites', JSON.stringify(favorites));
  }, [favorites]);

  const addToFavorites = (item: FavoriteItem) => {
    if (!favorites.some(favorite => favorite.showDateId === item.showDateId)) setFavorites(prevFavorites => [...prevFavorites, item]);
    else removeFromFavorites(item.showDateId);
  };

  const removeFromFavorites = (showDateId: number) => {
    setFavorites(prevFavorites => prevFavorites.filter(item => item.showDateId !== showDateId));
  };

  return (
    <FavoriteContext.Provider value={{ favorites, addToFavorites, removeFromFavorites }}>
      {children}
    </FavoriteContext.Provider>
  );
};

export const useFavorites = () => {
  const context = React.useContext(FavoriteContext);
  if (!context) {
    throw new Error('useFavorites must be used within a FavoriteProvider');
  }
  return context;
};