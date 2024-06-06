// StudyCarousel.js

import { useState } from 'react';
import Carousel from 'react-bootstrap/Carousel';

const StudyCarousel = ({ group, updateCard }) => {
    // these are called useState hooks, they allow components to keep track of some local state.
    // in this example, showFront holds true iff the card is shown
    // the second variable is used to change the state.
    // the true refers to the initialization state.
    const [showFront, setShowFront] = useState(true);

    const handleSlideChange = () => {
        console.log(2);
        setShowFront(true);
    };

    const handleCardFlip = () => {
        console.log(1);
        setShowFront(!showFront);
    };

    const handleCardResponse = (card, didSucceed) => {
        console.log(card);
        console.log(didSucceed);
        updateCard(card, didSucceed);
    };

    console.log(group);

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
                            {showFront ? card.question : card.answer}
                        </h3>
                        <div className="response-buttons">
                            <button onClick={() => handleCardResponse(card, true)}>OK</button>
                            <button onClick={() => handleCardResponse(card, false)}>PASS</button>
                        </div>
                    </div>
                </Carousel.Item>
            ))}
        </Carousel>
    );
};

export default StudyCarousel;
