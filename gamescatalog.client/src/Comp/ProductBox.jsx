import React from 'react';

function ProductBox({ game }) {
    return (
        <div className="border rounded-xl shadow-lg flex flex-col justify-between h-full">
            {/* Зображення гри */}
            <img src={game.previewUrl} alt={game.title} className="w-full h-48 object-cover rounded-tr-xl rounded-tl-xl" />

            <div className="flex flex-col flex-grow p-4">
                {/* Основна інформація про гру */}
                <div className='mb-2'>
                    <h3 className="text-xl font-bold mb-1">{game.title}</h3>
                    <p className="text-sm text-gray-500">Видавець: {game.publisher}</p>
                    <p className="text-sm text-gray-500">Розробник: {game.developer}</p>
                    
                    {/* Теги */}
                    <div className="mt-4">
                        <h4 className="text-sm font-semibold">Теги:</h4>
                        <ul className="flex flex-wrap gap-2 mt-1">
                            {game.tags.map((tag, index) => (
                                <li key={index} className="bg-blue-100 text-blue-800 text-xs px-2 py-1 rounded-full">
                                    {tag}
                                </li>
                            ))}
                        </ul>
                    </div>

                    {/* Платформи */}
                    <div className="mt-2">
                        <h4 className="text-sm font-semibold">Платформи:</h4>
                        <ul className="flex flex-wrap gap-2 mt-1">
                            {game.platforms.map((platform, index) => (
                                <li key={index} className="bg-green-100 text-green-800 text-xs px-2 py-1 rounded-full">
                                    {platform}
                                </li>
                            ))}
                        </ul>
                    </div>

                    {/* DLC */}
                    {game.isDLC && (
                        <div className="mt-4">
                            <h4 className="text-sm font-semibold text-purple-600">DLC:</h4>
                            <ul className="flex flex-wrap gap-2 mt-1">
                                {game.dlCs.map((dlc, index) => (
                                    <li key={index} className="bg-purple-100 text-purple-800 text-xs px-2 py-1 rounded-full">
                                        {dlc}
                                    </li>
                                ))}
                            </ul>
                        </div>
                    )}
                </div>

                {/* Рік і ціна, розміщені знизу */}
                <div className="mt-auto flex justify-between items-center pt-4 border-t  border-gray-200">
                    <p className="text-sm text-gray-500">{game.yearOfRelease}</p>
                    <p className="text-[18px] font-bold text-gray-700">{game.price} $</p>
                </div>
            </div>
        </div>
    );
}

export default ProductBox;
