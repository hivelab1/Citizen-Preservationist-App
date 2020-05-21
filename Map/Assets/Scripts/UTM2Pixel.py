import utm
import csv

# Parameters
width = 2534
height = 3218

# Callibration
# Barn approx. Pixel Coordinates
pxXA = 690
pxYA = 1180

# Barn WGS84
aWSG = [ 38.214124, -119.014525 ]


# Kirkwood approx. Pixel Coordinates
pxXB = 1875
pxYB = 2250

# Kirkwood WGS84
#bWSG = utm.to_latlon(mEB, mNB, 11, 'S')
bWSG = [ 38.209722, -119.007994 ]
# Compute Distance
# UTM
dYGlobal = aWSG[0] - bWSG[0]
dXGlobal = aWSG[1] - bWSG[1]

# Pixel
dXLocal = pxXA - pxXB
dYLocal = pxYA - pxYB

# Compute Scalar
xScalar = dXGlobal / dXLocal
yScalar = dYGlobal / dYLocal

# Compute Offsets
# Global Offset A
leftOffsetA = aWSG[1] - (xScalar * pxXA)
topOffsetA = aWSG[0] - (yScalar * pxYA)

# Global Offset B 
leftOffsetB = bWSG[1] - (xScalar * pxXB)
topOffsetB = bWSG[0] - (yScalar * pxYB)

# Global Offset Average
leftCornerGlobal = (leftOffsetA + leftOffsetB) / 2
topCornerGlobal = (topOffsetA + topOffsetB) / 2

print("Global Left WGS84 Corner: " + str(leftCornerGlobal))
print("Global Top WGS84 Corner: " + str(topCornerGlobal))

# Compute Pixel Ratios
# Pixel Ratio A
pxRatioAX = (aWSG[1] - leftCornerGlobal) / pxXA
pxRatioAY = (aWSG[0] - topCornerGlobal) / pxYA

# Pixel Ratio B 
pxRatioBX = (bWSG[1] - leftCornerGlobal) / pxXB
pxRatioBY = (bWSG[0] - topCornerGlobal) / pxYB

# Pixel Ratio Average
pxRatioX = (pxRatioAX + pxRatioBX) / 2
pxRatioY = (pxRatioAY + pxRatioBY) / 2

print("Pixel Ratio X: " + str(pxRatioX))
print("Pixel Ratio Y: " + str(pxRatioY))

with open('callibration.csv', 'w') as csvfile:
	writer = csv.writer(csvfile, delimiter=',')
	writer.writerow(['Left Corner Global', str(leftCornerGlobal)])
	writer.writerow(['Top Corner Global', str(topCornerGlobal)])
	
	writer.writerow(["Pixel Scalar X", str(pxRatioX)])
	writer.writerow(["Pixel Scalar Y", str(pxRatioY)])
	
# Test Time!!
# UTM Coordinates
#testEasting = 323765
#testNorthing = 4231376

# WSG Coordinates
#testCoordinates = utm.to_latlon(testEasting, testNorthing, 11, 'S')
testCoordinates = [ 38.212015, -119.014060 ]

# Compute WSG Offsets
testCoordOffsetX = testCoordinates[1] - leftCornerGlobal
testCoordOffsetY = testCoordinates[0] - topCornerGlobal

# Convert to Pixels
testCoordX = testCoordOffsetX / pxRatioX
testCoordY = testCoordOffsetY / pxRatioY

print(testCoordX)
print(testCoordY)
