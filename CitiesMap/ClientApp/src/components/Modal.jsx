import React, { Component } from 'react';

class Modal extends Component {
    constructor(props) {
        super(props);
        this.handleSave = this.handleSave.bind(this);
        this.state = {
            name: '',
            isNew: false
        }
    }

    componentWillReceiveProps(nextProps) {
        this.setState({
            name: nextProps.name,
            isNew: nextProps.isNew,
        });
    }

    nameHandler(e) {
        this.setState({ name: e.target.value });
    }


    handleSave() {
        //service post call
        const item = this.state;
        this.props.saveModalDetails(item)
    }

    render() {
        return (
            <div className="modal fade" id="cityModal" tabIndex="-1" role="dialog" aria-hidden="true" aria-labelledby="cityModalLabel">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title">City</h5>
                        </div>
                        <div className="modal-body">
                            <p><span className="modal-lable">Name:</span><input value={this.state.name} onChange={(e) => this.nameHandler(e)} /></p>
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-dismiss="modal">Close</button>
                            <button type="button" className="btn btn-primary" data-dismiss="modal" onClick={() => { this.handleSave() }}>Save</button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}

export default Modal;