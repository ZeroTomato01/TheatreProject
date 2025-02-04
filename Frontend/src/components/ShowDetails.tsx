import React from 'react';
import { useLocation, Link } from 'react-router-dom';
import { TheatreShow, TheatreShowDate } from './Shows';
import { Container, Row, Col, Card, Button } from 'react-bootstrap';
import './styles.css';

const ShowDetails: React.FC = () => {
  const location = useLocation();
  const show: TheatreShow = location.state?.show;
  const showDate: TheatreShowDate = location.state?.showDate; // Access the passed data

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
                  {showDate && (
                    <Card.Text>
                      <strong>Show Date:</strong> {new Date(showDate.dateAndTime).toLocaleString()}
                    </Card.Text>
                  )}
                </div>
              ) : (
                <p>No show data received.</p>
              )}
            </Card.Body>
          </Card>
          <Row className="justify-content-center">
            <Link to="/Shows">
                  <Button variant="primary">Go back</Button>
            </Link>
          </Row>
        </Col>
      </Row>
    </Container>
  );
};

export default ShowDetails;