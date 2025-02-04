import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import { TheatreShow, TheatreShowDate } from './Shows';
import { Container, Row, Col, Card, Button, ListGroup, Form } from 'react-bootstrap';
import { useCart } from './CartContext';
import './styles.css';

const ShowDetails: React.FC = () => {
  const location = useLocation();
  const show: TheatreShow = location.state?.show;
  const [showDates, setShowDates] = useState<TheatreShowDate[]>([]);
  const { addToCart } = useCart();
  const [ticketCounts, setTicketCounts] = useState<{ [key: number]: number }>({});

  useEffect(() => {
    if (show) {
      fetch("/TheatreShowDate/future")
        .then(response => response.json())
        .then(data => {
          // Sort show dates by date
          const sortedDates = data.sort((a: TheatreShowDate, b: TheatreShowDate) => new Date(a.dateAndTime).getTime() - new Date(b.dateAndTime).getTime());
          setShowDates(sortedDates);
        })
        .catch(error => console.error("Error fetching show dates:", error));
    }
  }, [show]);

  const handleTicketCountChange = (showDateId: number, count: number) => {
    setTicketCounts(prevCounts => ({
      ...prevCounts,
      [showDateId]: count
    }));
  };

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

  return (
    <Container className="mt-5">
      <Row className="justify-content-center">
        <Col md={8}>
          <Card>
            <Card.Header>
              <h1 className="text-center">Show Details</h1>
            </Card.Header>
            <Card.Body>
              {show ? (
                <div>
                  <Card.Title>{show.title}</Card.Title>
                  <Card.Text>
                    <strong>Description:</strong> {show.description}
                  </Card.Text>
                  <Card.Text>
                    <strong>Price:</strong> ${show.price}
                  </Card.Text>
                  <Card.Text>
                    <strong>Venue Id:</strong> {show.venueId}
                  </Card.Text>
                  <ListGroup variant="flush">
                    {showDates.map(showDate => (
                      <ListGroup.Item key={showDate.theatreShowDateId}>
                        <strong>Date:</strong> {new Date(showDate.dateAndTime).toLocaleString()}
                        <Form.Control
                          type="number"
                          min="1"
                          value={ticketCounts[showDate.theatreShowDateId] || 1}
                          onChange={(e) => handleTicketCountChange(showDate.theatreShowDateId, parseInt(e.target.value))}
                          className="mb-2"
                        />
                        <Button variant="primary" onClick={() => handleAddToCart(show, showDate)}>Reserve Ticket</Button>
                      </ListGroup.Item>
                    ))}
                  </ListGroup>
                </div>
              ) : (
                <p>No show data received.</p>
              )}
            </Card.Body>
          </Card>
          <Button variant="primary" href="/Shows" style={{ width: '10rem' }}>Go back</Button>
        </Col>
      </Row>
    </Container>
  );
};

export default ShowDetails;