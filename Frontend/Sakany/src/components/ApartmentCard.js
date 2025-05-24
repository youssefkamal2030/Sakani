// components/ApartmentCard.js
import React from 'react';
import './ApartmentCard.css';

const ApartmentCard = ({ image, title, location, price, guests, bedrooms}) => {
  return (
    <div className="apartment-card">
      <div className="image-container">
        <img src={image} alt={title} />
        <div className="price-tag"> {price} / شهر</div>
      </div>
      <div className="info">
      <h3>{title}</h3>
        <p className="location">{location}</p>
        
        <div className="details">
          <p>{guests} أفراد</p>
          <p>{bedrooms} غرف نوم</p>
        </div>
      </div>
    </div>
  );
};

export default ApartmentCard;
