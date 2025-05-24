import React from 'react';
import './HeroSection.css';
import FilterBar from './FilterBar';

const HeroSection = () => {
  return (
    <section className="hero-section">
      <div className="hero-content">
        <h1>سكنِي</h1>
        <p>سهولة.. ثقة.. أمان</p>
      </div>

      <div className="hero-filters">
        <FilterBar />
      </div>
    </section>
  );
};

export default HeroSection;
