import React, { Component } from 'react';
import Modal from './Modal'
import '../custom.css'

export class Cities extends Component {
    constructor(props) {
        super(props);
        this.state = {
            cities: [],
            index: 0,
            isnew: false
        }
        this.toggleModal = this.toggleModal.bind(this);

        this.saveModalDetails = this.saveModalDetails.bind(this);
    }

    componentDidMount() {
        this.getCitiesData();
    }
    

    toggleModal(id , isNew) {
        this.setState({
            isnew: isNew,
            index: id
        })
    }

    saveModalDetails(item) {
        //save to state
    }


    render() {
        var dataName = "";
        if (this.state.index !== 0) {
            dataName = this.state.cities.find(e => e.id == this.state.index).name
        }

        return (
            <div className="content-container">
                <h4>Cities</h4>
                <table className='table table-striped table-hover table-condensed'>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th> Action </th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.cities.map(city =>
                            <tr key={city.id}>
                                <td>{city.name}</td>
                                <td><button type="button" className="btn btn-light" data-toggle="modal" data target="#cityModal" onClick={() => this.toggleModal(city.id, false)}>Edit</button></td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <div class="btn-wrapper">
                    <button type="button" className="btn btn-light new-btn" data-toggle="modal" data-target="#cityModal" onClick={() => this.toggleModal(0, true)}>New City</button>
                </div>
                <Modal name={dataName} isNew={this.state.isnew} saveModalDetails={this.saveModalDetails}></Modal>
            </div>
        );
    }

    async getCitiesData() {
        const response = await fetch('GetCities');
        const data = await response.json();
        this.setState({ cities: data });
    }
}
