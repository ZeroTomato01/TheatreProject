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

export interface Venue {
  venueId?: number;
  name?: string;
  capacity?: number;
  theatreShowIds?: string;
}

export interface TheatreShowDate {
  theatreShowDateId: number;
  dateAndTime: string;
  theatreShowId: number;
}

const Shows: React.FC = () => {
  const [shows, setShows] = useState<TheatreShow[]>([]);
  const [showDates, setShowDates] = useState<TheatreShowDate[]>([]);
  const [venues, setVenues] = useState<Venue[]>([]);
  const { addToCart } = useCart();
  const [ticketCounts, setTicketCounts] = useState<{ [key: number]: number }>({});
  const { addToFavorites } = useFavorites();

  // Filter states
  const [titleFilter, setTitleFilter] = useState<string>('');
  const [priceFilter, setPriceFilter] = useState<number>(0);
  const [dateFilter, setDateFilter] = useState<string>('');

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

    // fetch venues
    fetch("api/Venue/GetAll")
      .then(response => response.json())
      .then(data => setVenues(data))
      .catch(error => console.error("Error fetching show dates:", error));
  }, []);

  // Filter shows based on criteria
  const filteredShows = shows.filter(show => {
    const matchesTitle = show.title?.toLowerCase().includes(titleFilter.toLowerCase());
    const matchesPrice = show.price !== undefined && show.price <= priceFilter;
    const matchesDate = showDates.some(date => {
      if (date.theatreShowId === show.theatreShowId) {
        const showDate = new Date(date.dateAndTime).toLocaleDateString('en-GB');
        return showDate === dateFilter;
      }
      return false;
    });

    // Apply filters
    return (
      (titleFilter === '' || matchesTitle) &&
      (priceFilter === 0 || matchesPrice) &&
      (dateFilter === '' || matchesDate)
    );
  });

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

  const getVenue = (venueId: number, venues: Venue[]) => {
    for(let i = 0; i < venues.length; i++) {
      if(venueId == venues[i].venueId)
      {
        return venues[i].name;
      }
    }
  };

  return (
    <Container>
      <h1>Shows</h1>
      <Row>
        {/* Filter Panel */}
        <Col md={3}>
          <Card className="mb-4">
            <Card.Body>
              <Card.Title>Filters</Card.Title>
              <Form>
                <Form.Group className="mb-3">
                  <Form.Label>Title</Form.Label>
                  <Form.Control
                    type="text"
                    placeholder="Search by title"
                    value={titleFilter}
                    onChange={(e) => setTitleFilter(e.target.value)}
                  />
                </Form.Group>
                <Form.Group className="mb-3">
                  <Form.Label>Price (Max)</Form.Label>
                  <Form.Control
                    type="number"
                    placeholder="Max price"
                    value={priceFilter}
                    onChange={(e) => setPriceFilter(parseFloat(e.target.value))}
                  />
                </Form.Group>
                <Form.Group className="mb-3">
                  <Form.Label>Date</Form.Label>
                  <Form.Control
                    type="date"
                    value={dateFilter}
                    onChange={(e) => setDateFilter(e.target.value)}
                  />
                </Form.Group>
              </Form>
            </Card.Body>
          </Card>
        </Col>

        {/* Shows List */}
        <Col md={9}>
          <Row>
            {filteredShows.length > 0 ? (
              filteredShows.map(show => (
                <Col key={show.theatreShowId} md={4}>
                  <Card className="mb-4">
                    <Card.Body>
                      <Card.Title>{show.title}</Card.Title>
                      <Card.Text>{show.description}</Card.Text>
                      <Card.Text><strong>Price:</strong> ${show.price}</Card.Text>
                      <Card.Text><strong>Venue:</strong> {getVenue(show.venueId ?? -1, venues)}</Card.Text>
                      <ListGroup variant="flush">
                        {showDates
                          .filter(date => date.theatreShowId === show.theatreShowId)                          .map(showDate => (
                            <ListGroup.Item key={showDate.theatreShowDateId}>
                              <strong>Date:</strong> {new Date(showDate.dateAndTime).toLocaleString()}
                              <Link 
                                to={"/ShowDetails"} 
                                state={{ show, showDate }}>
                                Details
                              </Link>
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
                  </Card>Í
                </Col>
              ))
            ) : (
              <Col>
                <p>No shows match the filters.</p>
              </Col>
            )}
          </Row>
        </Col>
      </Row>
    </Container>
  );
};

export default Shows;