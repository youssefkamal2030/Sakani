import React from 'react';
import './OurServicesSection.css';

const services = [
  {
    title: 'طلب معاينة',
    description: 'احجز سكنك بسهولة في أفضل المناطق القريبة من الجامعة.',
    
  },
  {
    title: 'الحصول على ختم الجودة',
    description: 'اطلبنا للتحقق من جودة سكنك المعروض للعرض ضمن الشقق المميزة',

  },
];

const OurServicesSection = () => {
  return (
    <section className="our-services">
      <h2 className="our-services-title">خدماتنا</h2>
      <p className="our-services-subtitle">
        نوفر لك خدمات متكاملة لمساعدتك في إيجاد السكن المثالي
      </p>
      <div className="services-cards">
        {services.map((service, index) => (
          <div key={index} className="service-card">

            <div className="service-info">
              <h3>{service.title}</h3>
              <p>{service.description}</p>
            </div>
          </div>
        ))}
      </div>
    </section>
  );
};

export default OurServicesSection;
