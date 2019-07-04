import 'bootstrap/dist/css/bootstrap.css';
import '../Styles/sku.css';
import React from 'react';
import Alert from 'react-bootstrap/Alert';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup'
import EasyReactComponent from './Base/EasyReactComponent';

const emptyString = '';

export default class Login extends EasyReactComponent {
    constructor(props, context) {
        super(props, context);

        this.handleLogin = this.handleLogin.bind(this);
        this.doLogin = props.doLogin;

        this.state = {
            show: true,
            showAlert: false,
            username: emptyString,
            password: emptyString
        };
    }

    async handleLogin() {
        let user = await this.doLogin(this.state.username, this.state.password);

        if (!!user) {
            this.setState({ show: false });
        }
        else {
            this.setState({
                message: 'Login failed',
                showAlert: true
            });
        }
    }

    render() {
        return (
            <div>
                <Modal
                    // {...this.props} // !!! Warning: React does not recognize the `doLogin` prop on a DOM element. If you intentionally want it to appear in the DOM as a custom attribute, spell it as lowercase `dologin` instead. If you accidentally passed it from a parent component, remove it from the DOM element.
                    size="sm"
                    aria-labelledby="contained-modal-title-vcenter"
                    backdrop="static"
                    centered="true"
                    show={this.state.show}
                    onHide={() => { }}
                >
                    <Modal.Header>
                        <Modal.Title>Login</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        <Form>
                            <Form.Group controlId="username">
                                <Form.Label>Username</Form.Label>
                                <InputGroup>
                                    <InputGroup.Prepend>
                                        <InputGroup.Text id="inputGroupPrepend">@</InputGroup.Text>
                                    </InputGroup.Prepend>
                                    <Form.Control
                                        type="text"
                                        placeholder="Username"
                                        aria-describedby="inputGroupPrepend"
                                        required
                                        value={this.state.username}
                                        onChange={this.onChange}
                                    />
                                    <Form.Control.Feedback type="invalid">
                                        Please choose a username.
                                    </Form.Control.Feedback>
                                </InputGroup>
                            </Form.Group>
                            <Form.Group controlId="password">
                                <Form.Label>Password</Form.Label>
                                <Form.Control
                                    type="password"
                                    placeholder="Password"
                                    required
                                    value={this.state.password}
                                    onChange={this.onChange} />
                            </Form.Group>
                        </Form>
                        <Alert show={this.state.showAlert} variant="danger">{this.state.message}</Alert>
                    </Modal.Body>
                    <Modal.Footer>
                        <Button variant="primary" onClick={this.handleLogin}>Login</Button>
                    </Modal.Footer>
                </Modal>
            </div>
        );
    }
}
