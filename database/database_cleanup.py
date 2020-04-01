import pandas as pd


top_40_airports_list = ['ATL', 'LAX', 'ORD', 'DFW', 'DEN', 'JFK', 'SFO', 'SEA', 'LAS', 'MCO', 'EWR', 'CLT', 'PHX', 'IAH', 'MIA', 'BOS', 'MSP', 'FLL', 'DTW', 'PHL', 'LGA', 'BWI', 'SLC', 'SAN', 'IAD', 'DCA', 'MDW', 'TPA', 'PDX', 'HNL']
top_10_airports_list = top_40_airports_list[0:10]


route_columns = ['Route ID', 'Source Airport', 'Source OpenFlights ID', 'Destination', 'Destination OpenFlights ID']

route_data_frame = pd.read_csv('~/Downloads/routes.csv')

top_10_routes = route_data_frame[(route_data_frame['Source Airport'].isin(top_10_airports_list)) & (route_data_frame['Destination'].isin(top_10_airports_list))]
top_10_routes = top_10_routes[route_columns]

top_10_routes.to_csv(path_or_buf='./top_10_routes.csv', index=False)


airport_columns = ['OpenFlights ID', 'Airport Name', 'City', 'IATA', 'Timezone', 'Tz database time zone']

airports_data_frame = pd.read_csv('~/Downloads/airports.csv')

top_10_airports = airports_data_frame[(airports_data_frame['IATA'].isin(top_10_airports_list))]
top_10_airports = top_10_airports[airport_columns]

top_10_airports.to_csv(path_or_buf='./top_10_airports.csv', index=False)