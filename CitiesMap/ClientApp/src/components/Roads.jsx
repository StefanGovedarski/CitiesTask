import React, { Component } from 'react';

export class Roads extends Component {
    constructor(props) {
        super(props);
        this.state = {
            roads: []
        }

        this.handleNewRoadClick = this.handleNewRoadClick.bind(this);
        this.handleEditRoadClick = this.handleEditRoadClick.bind(this);
    }

    componentDidMount() {
        this.getRoadsData();
    }

    handleNewRoadClick() {

    }

    handleEditRoadClick() {

    }

    render() {
        return (
            <div className="content-container">
                <h4>Roads</h4>
                <table className='table table-striped table-hover table-condensed'>
                    <thead>
                        <tr>
                            <th>Distance</th>
                            <th>From</th>
                            <th>To</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.roads.map(road =>
                            <tr key={road.id}>
                                <td>{road.distance}</td>
                                <td>{road.cityFrom}</td>
                                <td>{road.cityTo}</td>
                                <td><button type="button" className="btn btn-light" onClick={() => this.handleEditRoadClick()}>Edit</button></td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <div class="btn-wrapper">
                    <button type="button" className="btn btn-light new-btn" onClick={() => this.handleNewRoadClick()}>New Road</button>
                </div>
            </div>
        );
    }

    async getRoadsData() {
        const response = await fetch('GetRoads');
        const data = await response.json();
        this.setState({ roads: data });
    }
}
