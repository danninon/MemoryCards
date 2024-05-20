// StudyCarousel.js

import { useState } from 'react';
import Carousel from 'react-bootstrap/Carousel';

const StudyCarousel = ({ group }) => {
    // these are called useState hooks, they allow components to keep track of some local state.
    // in this example, showFront holds true iff the card is shown
    // the second variable is used to change the state.
    // the true refers to the initialization state.
    const [showFront, setShowFront] = useState(true);

    const handleSlideChange = () => {
        setShowFront(true);
    };

    const handleCardFlip = () => {
        setShowFront(!showFront);
    };


    //the carousel component returns changes its css attributes to change the UI depending on it's current value
    //note that cards className changes depending on the value of showFront, note that the classNames value may have more than one css value
    return (
        <Carousel
            hidden={!group || group.length === 0}
            onSlide={handleSlideChange}
        >
            {group.map((card, index) => (
                <Carousel.Item key={index} onClick={handleCardFlip}>
                    <div className={showFront ? 'card card-front' : 'card card-back flip'}> 
                        <h3 className={showFront ? 'content' : 'content flip'}>
                            {showFront ? card.front : card.back}
                        </h3>
                    </div>
                </Carousel.Item>
            ))}
        </Carousel>
    );
};

export default StudyCarousel;
