// src/pages/HomePage.js
import React from 'react';
import HeroSection from '../components/HeroSection';
import ApartmentsSection from '../sections/ApartmentsSection';
import OurServicesSection from '../sections/OurServicesSection';

const HomePage = () => {
  return (
    <div>
      <HeroSection />
      <ApartmentsSection />
      <OurServicesSection/>
    </div>
  );
};

export default HomePage;
