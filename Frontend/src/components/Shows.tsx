import React, { useState, useEffect } from 'react';
import { useCart } from './CartContext';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import { Button, Card, ListGroup, Container, Row, Col, Form } from 'react-bootstrap';
import { useFavorites } from './FavoriteContext';

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
    fetch("/Venue")
      .then(response => response.json())
      .then(data => setVenues(data))
      .catch(error => console.error("Error fetching show dates:", error));
  }, []);

  // Filter shows based on criteria
  const filteredShows = shows.filter(show => {
    const matchesTitle = show.title?.toLowerCase().includes(titleFilter.toLowerCase());
    const matchesPrice = show.price !== undefined && show.price <= priceFilter;
    // Find the first upcoming date for the show
    const sortedShowDates = showDates
      .filter(date => date.theatreShowId === show.theatreShowId)
      .sort((a, b) => new Date(a.dateAndTime).getTime() - new Date(b.dateAndTime).getTime());
    const firstShowDate = sortedShowDates[0];

    const matchesDate = firstShowDate ? new Date(firstShowDate.dateAndTime).toLocaleDateString('en-GB') === dateFilter : false;

    // Apply filters
    return (
      (titleFilter === '' || matchesTitle) &&
      (priceFilter === 0 || matchesPrice) &&
      (dateFilter === '' || matchesDate)
    );
  });


  const handleAddToFavorites = (show: TheatreShow, showDate: TheatreShowDate) => {
    addToFavorites({
      showDateId: showDate.theatreShowDateId,
      showTitle: show.title || "Untitled Show",
      showDate: new Date(showDate.dateAndTime).toLocaleString(),
      showPrice: show.price || 0
    });
  };

  const handleDetails = (show: TheatreShow) => (
    <Link
      to={"/ShowDetails"}
      state={{ show }}
      className="mr-2"
    >
      <Button variant="danger">
        Details
      </Button>
    </Link>
  );

  const getVenue = (venueId: number, venues: Venue[]) => {
    const venue = venues.find(v => v.venueId === venueId);
    return venue ? venue.name : 'Unknown Venue';
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
          filteredShows.map(show => {
            // Find the first upcoming date for the show
            const sortedShowDates = showDates
              .filter(date => date.theatreShowId === show.theatreShowId)
              .sort((a, b) => new Date(a.dateAndTime).getTime() - new Date(b.dateAndTime).getTime());
            const firstShowDate = sortedShowDates[0];

            return (
              <Col key={show.theatreShowId} md={4}>
                <Card className="mb-4">
                  <Card.Body>
                    <Card.Title>{show.title}</Card.Title>
                    <Card.Text>{show.description}</Card.Text>
                    <Card.Text><strong>Price:</strong> ${show.price}</Card.Text>
                    <Card.Text><strong>Venue:</strong> {getVenue(show.venueId ?? -1, venues)}</Card.Text>
                    {firstShowDate && (
                      <ListGroup variant="flush">
                        <ListGroup.Item>
                          <strong>Upcoming Showing:</strong>
                          <div>{new Date(firstShowDate.dateAndTime).toLocaleString()}</div>
                          <div className="mt-2">
                            {handleDetails(show)}
                            <Button variant="warning" onClick={() => handleAddToFavorites(show, firstShowDate)}>Fav</Button>
                          </div>
                        </ListGroup.Item>
                      </ListGroup>
                    )}
                  </Card.Body>
                </Card>
              </Col>
            );
          })
        ) : (
          <p>No shows available.</p>
        )}
        </Row>
      </Col>
    </Row>
  </Container>
  );
};

export default Shows;