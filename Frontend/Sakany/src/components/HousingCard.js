import React from 'react';
import './HousingCard.css';

const HousingCard = ({ image, title, location, price, type }) => {
  return (
    <div className="housing-card">
      <img src={"https://via.placeholder.com/300x180"
} alt={title} className="housing-card-img" />
      <div className="housing-card-content">
        <h3>{title}</h3>
        <p>{location}</p>
        <p className="price">{price}</p>
        <p className="type">{type}</p>
        <button>عرض التفاصيل</button>
      </div>
    </div>
  );
};

export default HousingCard;
