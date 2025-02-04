import React from "react";
import { Container, Row, Col, Card, Button } from "react-bootstrap";
import './styles.css';

const Home: React.FC = () => {
  return (
    <Container className="mt-5">
      <Row className="mb-4">
        <Col>
          <h1 className="text-center">Welcome to BerserkerCinema</h1>
          <p className="text-center">
            Experience the best movies in the most comfortable and most modern cinema in town.
          </p>
        </Col>
      </Row>
      <Row className="mb-4">
        <Col md={6}>
          <Card>
            <Card.Body>
              <Card.Title>About Us</Card.Title>
              <Card.Text>
                BerserkerCinema is dedicated to providing the best movie-going experience. Our state-of-the-art theaters are equipped with the latest technology to ensure you enjoy your favorite movies in the highest quality.
              </Card.Text>
              <Button variant="primary" href="/About">Learn More</Button>
            </Card.Body>
          </Card>
        </Col>
        <Col md={6}>
          <Card>
            <Card.Body>
              <Card.Title>Upcoming Shows</Card.Title>
              <Card.Text>
                Check out our upcoming shows and reserve your tickets now. Don't miss out on the latest blockbusters and exclusive screenings.
              </Card.Text>
              <Button variant="primary" href="/Shows">View Shows</Button>
            </Card.Body>
          </Card>
        </Col>
      </Row>
      <Row className="mb-4">
        <Col md={6}>
          <Card>
            <Card.Body>
              <Card.Title>Special Offers</Card.Title>
              <Card.Text>
                Take advantage of our special offers and discounts. Enjoy more movies for less with our exclusive deals.
              </Card.Text>
              <Button variant="primary" href="/Offers">See Offers</Button>
            </Card.Body>
          </Card>
        </Col>
        <Col md={6}>
          <Card>
            <Card.Body>
              <Card.Title>Contact Us</Card.Title>
              <Card.Text>
                Have any questions or need assistance? Get in touch with us and we'll be happy to help.
              </Card.Text>
              <Button variant="primary" href="/Contact">Contact Us</Button>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

export default Home;