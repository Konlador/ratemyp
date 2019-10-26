import React from 'react';
import { Container, Row } from 'react-bootstrap';
 
const layout = (props) => {
    return (
        <Container>
            <Row>
                This is the place for the navigation component.
            </Row>
            <main>
                {props.children}
            </main>
        </Container>
    )
}
export default layout;