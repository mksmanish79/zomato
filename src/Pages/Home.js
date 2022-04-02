import '../Resources/Css/home.css'

const Home = function () {
    document.title = 'Home';
    return (
        <div className="container-fluid homebanner">
            <div className='row'>
                <div className='col-lg-12'><span className='hometitle'>ZOMATO</span></div>
                <div className='col-lg-12'><span className='homesubtitle'>Discover the best food & drinks in Delhi NCR</span></div>
            </div>
        </div>
    )
}

export default Home;