import { useState } from 'react';
import Carousel from 'react-bootstrap/Carousel';

const StudyCarousel = ({ group, updateCard }) => {
    // useState hooks to keep track of some local state.
    const [currentIndex, setCurrentIndex] = useState(0);  // Index of the currently displayed card
    const [showFront, setShowFront] = useState(true);  // Whether the front of the card is shown

    // Handler for changing the slide, which also resets to showing the front of the card
    const handleCardSelection = (selectedIndex, e) => {
        console.log(`selectedIndex: ${selectedIndex}`)
        setCurrentIndex(selectedIndex);
        setShowFront(true);
    };

    // Toggles between showing the front and the back of the card
    const handleCardFlip = () => {
        console.log(`showFront: ${showFront}`)
        setShowFront(!showFront);  // Toggle view between front and back
    };

    // Handles the response when either 'OK' or 'PASS' is clicked
    const handleCardResponse = (didSucceed) => {
        const card = group[currentIndex];  // Get the current card based on currentIndex
        console.log(`card: ${JSON.stringify(card)}`);
        console.log(`didSucceed: ${didSucceed}`);
        updateCard(card, didSucceed);
    };

    return (
        <>
            <Carousel activeIndex={currentIndex} onSelect={handleCardSelection} interval={null}>
                {group.map((card, index) => (
                    <Carousel.Item key={index} onClick={handleCardFlip}>
                        <div className={showFront ? 'card card-front' : 'card card-back'}>
                            <h3 className={showFront ? 'content' : 'content flip'}>
                                {showFront ? card.question : card.answer}
                            </h3>
                        </div>
                    </Carousel.Item>
                ))}
            </Carousel>
            <div className="response-buttons">
                <button onClick={() => handleCardResponse(true)}>OK</button>
                <button onClick={() => handleCardResponse(false)}>PASS</button>
            </div>
        </>
    );
};

export default StudyCarousel;
