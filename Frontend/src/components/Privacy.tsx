import React from 'react';
import { Container, Row, Col, Card } from 'react-bootstrap';
import './styles.css';

const Privacy: React.FC = () => {
  return (
    <Container className="mt-5">
      <Row className="mb-4">
        <Col>
          <h1 className="text-center">Privacy Agreement</h1>
          <p className="text-center">
            Your privacy is important to us. This privacy statement explains the personal data BerserkerCinema processes, how BerserkerCinema processes it, and for what purposes. We don't sell any data, we swear.
          </p>
        </Col>
      </Row>
      <Row className="mb-4">
        <Col>
          <Card>
            <Card.Body>
              <Card.Title>Information We Collect</Card.Title>
              <Card.Text>
                We collect information to provide better services to all our users. We collect information in the following ways:
                <ul>
                  <li>Information you give us. For example, our services require you to sign up for an account. When you do, we’ll ask for personal information, like your name, email address, and telephone number.</li>
                  <li>Information we get from your use of our services. We collect information about the services that you use and how you use them, like when you visit a website that uses our advertising services or view and interact with our ads and content.</li>
                </ul>
              </Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>
      <Row className="mb-4">
        <Col>
          <Card>
            <Card.Body>
              <Card.Title>How We Use Information</Card.Title>
              <Card.Text>
                We use the information we collect from all of our services to provide, maintain, protect, and improve them, to develop new ones, and to protect BerserkerCinema and our users. We also use this information to offer you tailored content – like giving you more relevant search results and ads.
              </Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>
      <Row className="mb-4">
        <Col>
          <Card>
            <Card.Body>
              <Card.Title>Information We Share</Card.Title>
              <Card.Text>
                We do not share personal information with companies, organizations, and individuals outside of BerserkerCinema unless one of the following circumstances applies:
                <ul>
                  <li>With your consent. We will share personal information with companies, organizations, or individuals outside of BerserkerCinema when we have your consent to do so.</li>
                  <li>For legal reasons. We will share personal information with companies, organizations, or individuals outside of BerserkerCinema if we have a good-faith belief that access, use, preservation, or disclosure of the information is reasonably necessary to meet any applicable law, regulation, legal process, or enforceable governmental request.</li>
                </ul>
              </Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>
      <Row className="mb-4">
        <Col>
          <Card>
            <Card.Body>
              <Card.Title>Contact Us</Card.Title>
              <Card.Text>
                If you have any questions about this Privacy Policy, please contact us:
                <ul>
                  <li>By email: privacy@berserkercinema.com</li>
                  <li>By visiting this page on our website: <a href="/Contact">Contact Us</a></li>
                </ul>
              </Card.Text>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

export default Privacy;