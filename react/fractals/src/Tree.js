import "./Tree.css";

const angleDiff = 16;

export default function Tree({
	depth = 10,
	length = 150,
	angle = 0,
	reductionFactor = 0.8,
	thickness = 20,
}) {
	if (depth <= 1) return null;

	const childLength = length * reductionFactor;
	const childThickness = thickness * 0.5;

	return (
		<div
			className="tree"
			style={{
				rotate: `${angle}deg`,
				height: thickness,
			}}
		>
			<div className="branch" style={{ width: length }}></div>
			<div>
				<Tree
					depth={depth - 1}
					length={childLength}
					angle={-angleDiff}
					reductionFactor={reductionFactor}
					thickness={childThickness}
				/>
				<Tree
					depth={depth - 1}
					length={childLength}
					angle={angleDiff}
					reductionFactor={reductionFactor}
					thickness={childThickness}
				/>
			</div>
		</div>
	);
}
