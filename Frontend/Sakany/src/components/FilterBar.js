import React from 'react';
import './FilterBar.css';

const FilterBar = () => {
  return (
    <div className="filter-bar">
      <select>
        <option>اختر المنطقة</option>
        <option>عمر مكرم</option>
        <option>المكتبات</option>
        <option>يسري راغب</option>
        <option>مصنع سيد</option>
        <option>الأزهر</option>
        <option>فريال</option>
        <option>الجمهورية</option>
        <option>النميس</option>
        <option>الهلالي</option>
        <option>الوليدية</option>
        <option>النزلة</option>
        <option>ميدان الميدوب</option>
        <option>الأربعين</option>
      </select>

      <select>
        <option>النوع</option>
        <option>طلبة</option>
        <option>طالبات</option>
      </select>

      <button>بحث</button>
    </div>
  );
};

export default FilterBar;
