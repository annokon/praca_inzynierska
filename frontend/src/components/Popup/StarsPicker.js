import React, { useMemo } from "react";

export default function StarsPicker({ value, onChange, max = 10, className = "" }) {
    const v = Math.max(0, Math.min(Number(value) || 0, max));

    const stars = useMemo(() => Array.from({ length: max }, (_, i) => i + 1), [max]);

    return (
        <div className={`stars-picker ${className}`}>
            <div className="stars-picker__stars" role="radiogroup" aria-label={`Ocena ${v} na ${max}`}>
                {stars.map((n) => {
                    const filled = n <= v;
                    return (
                        <button
                            key={n}
                            type="button"
                            className="stars-picker__star"
                            onClick={() => onChange(n)}
                            role="radio"
                            aria-checked={v === n}
                            aria-label={`Ustaw ocenę ${n} na ${max}`}
                            title={`${n}/${max}`}
                        >
                            {filled ? "★" : "☆"}
                        </button>
                    );
                })}
            </div>

            <div className="stars-picker__count">{v}/{max}</div>
        </div>
    );
}
