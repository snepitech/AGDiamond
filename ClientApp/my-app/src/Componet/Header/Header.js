import './header.css';
import '../commen.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { Navbar, Nav, NavDropdown, Container } from 'react-bootstrap';
import { Link } from "react-router-dom";
import logo from '../../images/Logo.png';

const Header = props => {
    return (
        <>
            <div>
                <Navbar collapseOnSelect expand="lg" bg="" variant="dark" fixed="top" id="navbar">
                    <Container fluid className='navbar'>
                        <Link to="/"><img src={logo} className='nav_logo' /></Link>
                        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                        <Navbar.Collapse id="responsive-navbar-nav">
                            <Nav className="me-auto"></Nav>
                            <Nav className='nav align-items-center'>
                                <Nav.Link href="#dashboard"><Link to="/" className='nav_ser'>DASHBOARD</Link></Nav.Link>
                                <Nav.Link href="#cart"><Link to="/Cart" className='nav_ser'>CART</Link></Nav.Link>
                                <Nav.Link href="#memo"><Link to="/Memo " className='nav_ser'>MEMO</Link></Nav.Link>
                                <Nav.Link href="#invoice"><Link to="/invoice" className='nav_ser'>INVOICE</Link></Nav.Link>
                                <Nav.Link href="#request"><Link to="/request" className='nav_ser'>REQUEST</Link></Nav.Link>
                                <Nav.Link href="#Career"><Link to="/career" className='nav_ser'>CAREER</Link></Nav.Link>
                                <NavDropdown title="ABOUT US" id="collasible-nav-dropdown" className='nav_ser'>
                                    <NavDropdown.Item href="#PagePermission"><Link to="/PagePermission" className='nav_ser1'>Page Permission</Link></NavDropdown.Item>
                                    <NavDropdown.Item href="#Role"><Link to="/Role" className='nav_ser1'>Role</Link></NavDropdown.Item>
                                    <NavDropdown.Item href="#Setting"><Link to="/setting" className='nav_ser1'>Setting</Link></NavDropdown.Item>
                                </NavDropdown>
                            </Nav>
                        </Navbar.Collapse>
                    </Container>
                </Navbar>
            </div>
        </>
    )
}

export default Header;