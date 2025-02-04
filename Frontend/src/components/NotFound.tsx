import React from 'react';
import { Container, Row, Col, Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';

const NotFound: React.FC = () => {
    return (
        <Container className="d-flex flex-column justify-content-center align-items-center vh-100">
          <Row>
            <Col className="text-center">
              <h1 className="display-1 text-danger">404</h1>
              <h2 className="mb-4">Page Not Found</h2>
              <p className="mb-4">Sorry, the page you are looking for does not exist. Please notify an admin immediately.</p>
              <Link to="/Home">
                <Button variant="primary">Go to Home</Button>
              </Link>
            </Col>
          </Row>
        </Container>
      );
    };

export default NotFound;