import React from 'react';
import ApartmentCard from '../components/ApartmentCard';
import './ApartmentsSection.css';

const apartments = [
  {
    image: require('../assets/room1.jpg'),
    location: 'أسيوط، بوابة عمر مكرم',
    title: 'شقة فاخرة بأسيوط',
    price: '1250 جنيه',
    guests: 4,
    bedrooms: 2,

  },
  {
    image: require('../assets/room2.jpg'),
    title: 'سكن مميز للطالبات',
    location: 'أسيوط، مصنع سييد شارع السبيل',
    price: '700 جنيه',
    guests: 6,
    bedrooms: 3,

  },
  {
    image: require('../assets/room3.jpg'),
    title: 'سكن طلبة مكيف',
    location: 'أسيوط، شارع المكتبات',
    price: '950 جنيه',
    guests: 8,
    bedrooms: 4,

  },
  {
    image: require('../assets/room4.jpg'),
    title: 'غرفة ثنائية فاخرة',
    location: 'أسيوط، الأزهر تقسيم الحقوقيين',
    price: '1000 جنيه',
    guests: 6,
    bedrooms: 3,

  }
];

const ApartmentsSection = () => {
  return (
    <section className="apartments-section">
      <h2 className="section-title">الشقق المميزة</h2>
      <p className="section-subtitle">استكشف أفضل خيارات السكن المتاحة للطلبة في أسيوط</p>
      <div className="apartments-grid">
        {apartments.map((apartment, index) => (
          <ApartmentCard key={index} {...apartment} />
        ))}
      </div>
    </section>
  );
};

export default ApartmentsSection;
