import React, { useState, useEffect } from 'react';
import { useCart } from './CartContext';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import { Button, Card, ListGroup, Container, Row, Col, Form } from 'react-bootstrap';
import { FaStar } from 'react-icons/fa';
import { useFavorites } from './FavoriteContext';

import Reserve from './Reserve';

export interface TheatreShow {
  theatreShowId?: number;
  title?: string;
  description?: string;
  price?: number;
  theatreShowDateIds?: number[];
  venue?: { name: string };
  venueId?: number;
  
 
}

export interface TheatreShowDate {
  theatreShowDateId: number;
  dateAndTime: string;
  theatreShowId: number;
}

const Shows: React.FC = () => {
  const [shows, setShows] = useState<TheatreShow[]>([]);
  const [showDates, setShowDates] = useState<TheatreShowDate[]>([]);
  const { addToCart } = useCart();
  const [ticketCounts, setTicketCounts] = useState<{ [key: number]: number }>({});
  const { addToFavorites } = useFavorites();
  //maybe just add amin check, so we can use this same component in the admin dashboard?
  
  
  useEffect(() => {
    // Fetch TheatreShows
    fetch("/TheatreShow")
      .then(response => response.json())
      .then(data => setShows(data))
      .catch(error => console.error("Error fetching shows:", error));

    // Fetch TheatreShowDates separately
    fetch("/TheatreShowDate/future")
      .then(response => response.json())
      .then(data => setShowDates(data))
      .catch(error => console.error("Error fetching show dates:", error));
  }, []);

  const handleAddToCart = (show: TheatreShow, showDate: TheatreShowDate) => {
    const count = ticketCounts[showDate.theatreShowDateId] || 1;
    for (let i = 0; i < count; i++) {
      addToCart({
        showDateId: showDate.theatreShowDateId,
        showTitle: show.title || "Untitled Show",
        showDate: new Date(showDate.dateAndTime).toLocaleString(),
        showPrice: show.price || 0
      });
    }
  };

  const handleTicketCountChange = (showDateId: number, count: number) => {
    setTicketCounts(prevCounts => ({
      ...prevCounts,
      [showDateId]: count
    }));
  };

  const handleAddToFavorites = (show: TheatreShow, showDate: TheatreShowDate) => {
    addToFavorites({
      showDateId: showDate.theatreShowDateId,
      showTitle: show.title || "Untitled Show",
      showDate: new Date(showDate.dateAndTime).toLocaleString(),
      showPrice: show.price || 0
    });
  };

  return (
    <Container>
      <h1>Shows</h1>
      <Row>
        {shows.length > 0 ? (
          shows.map(show => (
            <Col key={show.theatreShowId} md={4}>
              <Card className="mb-4">
                <Card.Body>
                  <Card.Title>{show.title}</Card.Title>
                  <Card.Text>{show.description}</Card.Text>
                  <Card.Text><strong>Price:</strong> ${show.price}</Card.Text>
                  <Card.Text><strong>Venue:</strong> {show.venue?.name}</Card.Text>
                  <ListGroup variant="flush">
                    {showDates
                      .filter(date => date.theatreShowId === show.theatreShowId)
                      .map(showDate => (
                        <ListGroup.Item key={showDate.theatreShowDateId}>
                          <strong>Date:</strong> {new Date(showDate.dateAndTime).toLocaleString()}
                          <Link to={`/Reserve/${showDate.theatreShowDateId}`}> Reserve</Link>
                          <Form.Control
                            type="number"
                            min="1"
                            value={ticketCounts[showDate.theatreShowDateId] || 1}
                            onChange={(e) => handleTicketCountChange(showDate.theatreShowDateId, parseInt(e.target.value))}
                            className="mb-2"
                          />
                          <Button variant="primary" onClick={() => handleAddToCart(show, showDate)}>Reserve Ticket</Button>
                          <Button variant="warning" onClick={() => handleAddToFavorites(show, showDate)} className="ml-2">Fav</Button>
                        </ListGroup.Item>
                      ))}
                  </ListGroup>
                </Card.Body>
              </Card>
            </Col>
          ))
        ) : (
          <Col>
            <p>Loading shows...</p>
          </Col>
        )}
      </Row>
    </Container>
  );
};

export default Shows;