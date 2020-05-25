class Phone extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.phone };
        this.onClick = this.onClick.bind(this);
    }
    onClick(e) {
        this.props.onRemove(this.state.data);
    }
    render() {
        return <div className="card mt-4 mb-4">
            <div className="card-body">
                <div className="container pt-2 pb-0">
                    <div className="row">
                        <div className="col-auto .mr-auto card-title">
                            <h4 className="m-auto">
                                {this.state.data.departureTime}
                            </h4>
                        </div>
                        <div className="col-auto mr-0 ml-auto pt-0">
                            <Badge priority={this.state.data.priority} />
                        </div>
                    </div>
                    <div className="row ">
                        <div className="col-auto .mr-auto">
                            <label className="m-auto">
                                {this.state.data.startPoint}
                            </label>
                        </div>
                    </div>
                    <div className="row p-0">
                        <div className="col-auto .mr-auto">
                            <svg className="bi bi-arrow-down" width="1em" height="1em" viewBox="0 0 16 16"
                                fill="currentColor"
                                xmlns="http://www.w3.org/2000/svg">
                                <path fill-rule="evenodd"
                                    d="M4.646 9.646a.5.5 0 01.708 0L8 12.293l2.646-2.647a.5.5 0 01.708.708l-3 3a.5.5 0 01-.708 0l-3-3a.5.5 0 010-.708z"
                                    clip-rule="evenodd" />
                                <path fill-rule="evenodd" d="M8 2.5a.5.5 0 01.5.5v9a.5.5 0 01-1 0V3a.5.5 0 01.5-.5z"
                                    clip-rule="evenodd" />
                            </svg>
                        </div>
                    </div>
                    <div className="row p-0 ">
                        <div className="col-auto mr-auto">
                            <label className="m-auto">
                                {this.state.data.finishPoint}
                            </label>
                        </div>
                    </div>
                    <div className="row mb-0 pb-0">
                        <div className="col-auto .mr-auto card-title">
                            <p className="card-text ml-0 mt-2"><small className="text-muted">Заказ был размещен в {this.state.data.orderTime}
                            </small></p>
                        </div>
                        <div className="col-auto mr-0 ml-auto pt-0 mb-0">
                            <div className="btn-group" role="group" aria-label="Basic example">
                                <button type="button" onClick={this.onClick} class="btn btn-outline-dark pt-0 pb-0  pl-3 pr-3">
                                    <svg className="bi bi-trash-fill" width="0.8em" height="0.8em" viewBox="0 0 16 16"
                                        fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                        <path fill-rule="evenodd"
                                            d="M2.5 1a1 1 0 00-1 1v1a1 1 0 001 1H3v9a2 2 0 002 2h6a2 2 0 002-2V4h.5a1 1 0 001-1V2a1 1 0 00-1-1H10a1 1 0 00-1-1H7a1 1 0 00-1 1H2.5zm3 4a.5.5 0 01.5.5v7a.5.5 0 01-1 0v-7a.5.5 0 01.5-.5zM8 5a.5.5 0 01.5.5v7a.5.5 0 01-1 0v-7A.5.5 0 018 5zm3 .5a.5.5 0 00-1 0v7a.5.5 0 001 0v-7z"
                                            clip-rule="evenodd" />
                                    </svg>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>;
    }
}

class PhonesList extends React.Component {

    constructor(props) {
        super(props);
        this.state = { phones: [], count: 0 };
        this.onRemovePhone = this.onRemovePhone.bind(this);
    }
    loadData() {
        var xhr = new XMLHttpRequest();
        let c = 0;
        xhr.open("get", this.props.get, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ phones: data });
            c = data.length;
        }.bind(this);
        xhr.send();
        this.setState({ count: c });
    }
    componentDidMount() {
        this.loadData();
    }
    onRemovePhone(phone) {

        if (phone) {
            var data = new FormData();
            data.append("id", phone.id);

            var xhr = new XMLHttpRequest();
            xhr.open("delete", this.props.delete, true);
            xhr.onload = function () {
                if (xhr.status == 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send(data);
        }
    }
    render() {

        var remove = this.onRemovePhone;
        return <div>
            <Title count={this.state.count} />
            <div>
                {
                    this.state.phones.map(function (phone) {

                        return <Phone key={phone.id} phone={phone} onRemove={remove} />
                    })
                }
            </div>
        </div>;
    }
}

class Title extends React.Component {
    constructor(props) {
        super(props);
    }

    render() {
        return <div><div id="titlesearch">
            <div class="radio-group flex-row d-flex flex-row mt-3 mb-5 justify-content-between align-content-center">
                <div class="align-self-center">
                    <h1 class="title my-auto">Найдено<span class="badge badge-primary transition  ml-sm-3"></span></h1>
                </div>
                <Badge priority={2} />
                <Badge priority={1} />
                <Badge priority={0} />

                <input hidden id="priorityfilter" type="text" name="radio-value" />
            </div>
        </div>
            <div id="titleactual">
                <h1 class="title mt-3 mb-5 ">Оформленные заказы<span class="badge badge-primary ml-sm-3 ">{this.props.count}</span></h1>
            </div>
        </div>;
    }
}

class Badge extends React.Component {
    constructor(props) {
        super(props);
        this.state = { lvl: this.props.priority };
    }
    render() {
        if (this.state.lvl == 0)
            return <div data-value="low" class="my-auto align-self-center radio">
                <span class="radio badge badge-pill badge-secondary transition low">low</span>
            </div>;
        if (this.state.lvl == 1)
            return <div data-value="medium" class="my-auto align-self-center radio">
                <span class="radio badge badge-pill badge-warning transition medium">medium</span>
            </div>;
        else
            return <div data-value="high" class="my-auto align-self-center radio">
                <span class="radio badge badge-pill transition text-white high">high</span>
            </div>;

    }
}

ReactDOM.render(
    <PhonesList get="/order/getactuals" delete="/order/delete" />,
    document.getElementById("content")
);