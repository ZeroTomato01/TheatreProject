import React from 'react';
import { useFavorites } from './FavoriteContext';
import { Button, Offcanvas, ListGroup } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';

interface FavoriteDrawerProps {
  show: boolean;
  onHide: () => void;
}

const FavoriteDrawer: React.FC<FavoriteDrawerProps> = ({ show, onHide }) => {
  const { favorites, removeFromFavorites } = useFavorites();
  const navigate = useNavigate();

  const handleDetails = (item: any) => {
    navigate("/ShowDetails", { state: { show: item } });
    onHide();
  };

  return (
    <Offcanvas show={show} onHide={onHide} placement="start">
      <Offcanvas.Header closeButton>
        <Offcanvas.Title>Favorites</Offcanvas.Title>
      </Offcanvas.Header>
      <Offcanvas.Body>
        <ListGroup variant="flush">
          {favorites.map((item, index) => (
            <ListGroup.Item key={`${item.showDateId}-${index}`}>
              <div className="d-flex justify-content-between align-items-center">
                <div>
                  <strong>{item.showTitle}</strong>
                </div>
                <div>
                  <Button variant="primary" size="sm" onClick={() => handleDetails(item)}>Details</Button>
                  <Button variant="danger" size="sm" onClick={() => removeFromFavorites(item.showDateId)}>Remove</Button>
                </div>
              </div>
            </ListGroup.Item>
          ))}
        </ListGroup>
      </Offcanvas.Body>
    </Offcanvas>
  );
};

export default FavoriteDrawer;