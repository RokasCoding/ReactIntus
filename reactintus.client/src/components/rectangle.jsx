import React, { useState, useEffect, useRef } from 'react';

const Rectangle = () => {
    const [dimensions, setDimensions] = useState({ width: 150, height: 250 }); // Initialize with default values
    const [error, setError] = useState('');
    const [isResizing, setIsResizing] = useState(false);
    const svgRef = useRef(null);

    useEffect(() => {
        fetch('/api/Rectangle')
            .then(response => {
                if (!response.ok) {
                    return response.text().then(text => { throw new Error(text); });
                }
                return response.json();
            })
            .then(data => setDimensions(data))
            .catch(err => setError(err.message));
    }, []);

    const handleMouseUp = () => {
        if (isResizing) {
            setIsResizing(false);
            fetch('/api/Rectangle', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(dimensions)
            })
                .then(response => {
                    if (!response.ok) {
                        return response.text().then(text => { throw new Error(text); });
                    }
                    return response.json();
                })
                .then(data => {
                    setError('');
                    setDimensions(data);
                })
                .catch(err => setError(err.message));
        }
    };

    const handleMouseMove = (e) => {
        if (!isResizing) return;
        const svgRect = svgRef.current.getBoundingClientRect();
        const newWidth = e.clientX - svgRect.left;
        const newHeight = e.clientY - svgRect.top;

        setDimensions({
            width: Math.max(10, Math.floor(newWidth)),
            height: Math.max(10, Math.floor(newHeight))
        });
    };

    const startResizing = () => {
        setIsResizing(true);
    };

    const perimeter = 2 * (dimensions.width + dimensions.height);

    return (
        <div
            onMouseMove={handleMouseMove}
            onMouseUp={handleMouseUp}
            style={{ userSelect: 'none' }}
        >
            <svg
                ref={svgRef}
                width="500"
                height="500"
                onMouseLeave={() => isResizing && setIsResizing(false)}
            >
                <rect
                    x="0"
                    y="0"
                    width={dimensions.width}
                    height={dimensions.height}
                    fill="green"
                />
                {/* Resize Handle */}
                <circle
                    cx={dimensions.width}
                    cy={dimensions.height}
                    r="4"
                    fill="gray"
                    style={{ cursor: 'se-resize' }}
                    onMouseDown={startResizing}
                />
            </svg>
            <p>Perimeter: {perimeter}</p>
            {error && <p style={{ color: 'red' }}>{error}</p>}
        </div>
    );
};

export default Rectangle;
