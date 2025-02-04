import React from 'react';
import { useFavorites } from './FavoriteContext';
import { Badge, Button } from 'react-bootstrap';
import { FaStar } from 'react-icons/fa';

interface FavoriteIconProps {
  onClick: () => void;
}

const FavoriteIcon: React.FC<FavoriteIconProps> = ({ onClick }) => {
  const { favorites } = useFavorites();

  return (
    <Button variant="link" onClick={onClick} style={{ position: 'relative' }}>
      <FaStar size={24} />
      {favorites.length > 0 && (
        <Badge pill bg="warning" style={{ position: 'absolute', top: 0, right: 0 }}>
          {favorites.length}
        </Badge>
      )}
    </Button>
  );
};

export default FavoriteIcon;