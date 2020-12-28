﻿// Crest Ocean System

// This file is subject to the MIT License as seen in the root of this folder structure (LICENSE)

using UnityEngine;
using Crest.Spline;

namespace Crest
{
    public static class ShapeGerstnerSplineHandling
    {
        public static bool GenerateMeshFromSpline(Spline.Spline spline, Transform transform, int subdivisions, float radius, int smoothingIterations, ref Mesh mesh)
        {
            var splinePoints = spline.SplinePoints;
            if (splinePoints.Length < 2) return false;

            var points = new Vector3[(splinePoints.Length - 1) * 3 + 1];
            for (int i = 0; i < points.Length; i++)
            {
                float tm = 0.39f;

                if (i % 3 == 0)
                {
                    points[i] = splinePoints[i / 3].transform.position;
                }
                else if (i % 3 == 1)
                {
                    var idx = i / 3;
                    var tangent = TangentAfter(splinePoints, idx);
                    tangent = tangent.normalized * (splinePoints[i / 3 + 1].transform.position - splinePoints[i / 3].transform.position).magnitude;
                    points[i] = splinePoints[idx].transform.position + tm * tangent;

                    if (i == 1)
                    {
                        tangent = TangentBefore(splinePoints, idx + 1);
                        // Mirror first tangent
                        var toNext = (splinePoints[idx + 1].transform.position - splinePoints[idx].transform.position).normalized;
                        var nearestPoint = Vector3.Dot(tangent, toNext) * toNext;
                        tangent += 2f * (nearestPoint - tangent);
                        tangent = tangent.normalized * (splinePoints[i / 3 + 1].transform.position - splinePoints[i / 3].transform.position).magnitude;
                        points[i] = splinePoints[idx].transform.position + tm * tangent;
                    }
                }
                else
                {
                    var idx = i / 3 + 1;
                    var tangent = TangentBefore(splinePoints, idx);
                    tangent = tangent.normalized * (splinePoints[i / 3 + 1].transform.position - splinePoints[i / 3].transform.position).magnitude;
                    points[i] = splinePoints[idx].transform.position - tm * tangent;

                    if (i == points.Length - 2)
                    {
                        tangent = TangentAfter(splinePoints, idx - 1);
                        // Mirror first tangent
                        var toNext = (splinePoints[idx - 1].transform.position - splinePoints[idx].transform.position).normalized;
                        var nearestPoint = Vector3.Dot(tangent, toNext) * toNext;
                        tangent += 2f * (nearestPoint - tangent);
                        tangent = tangent.normalized * (splinePoints[i / 3 + 1].transform.position - splinePoints[i / 3].transform.position).magnitude;
                        points[i] = splinePoints[idx].transform.position - tm * tangent;
                    }
                }
            }

            if (splinePoints.Length > 1)
            {
                float lengthEst = 0f;
                for (int i = 1; i < splinePoints.Length; i++)
                {
                    lengthEst += (splinePoints[i].transform.position - splinePoints[i - 1].transform.position).magnitude;
                }
                lengthEst = Mathf.Max(lengthEst, 1f);

                float spacing = 16f / Mathf.Pow(2f, subdivisions + 2);
                int pointCount = Mathf.CeilToInt(lengthEst / spacing);
                pointCount = Mathf.Max(pointCount, 1);

                var resultPts0 = new Vector3[pointCount];

                resultPts0[0] = points[0];
                for (int i = 1; i < pointCount; i++)
                {
                    float t = i / (float)(pointCount - 1);

                    var tpts = t * (splinePoints.Length - 1);
                    var spidx = Mathf.FloorToInt(tpts);
                    var alpha = tpts - spidx;
                    if (spidx == splinePoints.Length - 1)
                    {
                        spidx -= 1;
                        alpha = 1f;
                    }
                    var pidx = spidx * 3;

                    resultPts0[i] = (1 - alpha) * (1 - alpha) * (1 - alpha) * points[pidx] + 3 * alpha * (1 - alpha) * (1 - alpha) * points[pidx + 1] + 3 * alpha * alpha * (1 - alpha) * points[pidx + 2] + alpha * alpha * alpha * points[pidx + 3];
                }

                var resultPts1 = new Vector3[pointCount];
                for (int i = 0; i < pointCount; i++)
                {
                    var tangent = resultPts0[Mathf.Min(pointCount - 1, i + 1)] - resultPts0[Mathf.Max(0, i - 1)];
                    var normal = tangent;
                    normal.x = tangent.z;
                    normal.z = -tangent.x;
                    normal = normal.normalized;
                    resultPts1[i] = resultPts0[i] + normal * radius;
                }

                var resultPtsTmp = new Vector3[pointCount];
                for (int j = 0; j < smoothingIterations; j++)
                {
                    resultPtsTmp[0] = resultPts1[0];
                    resultPtsTmp[pointCount - 1] = resultPts1[pointCount - 1];
                    for (int i = 1; i < pointCount - 1; i++)
                    {
                        resultPtsTmp[i] = (resultPts1[i] + resultPts1[i + 1] + resultPts1[i - 1]) / 3f;
                        resultPtsTmp[i] = resultPts0[i] + (resultPtsTmp[i] - resultPts0[i]).normalized * radius;
                    }
                    var tmp = resultPts1;
                    resultPts1 = resultPtsTmp;
                    resultPtsTmp = tmp;
                }

                return UpdateMesh(transform, resultPts0, resultPts1, ref mesh);
            }

            return false;
        }

        static Vector3 TangentAfter(SplinePoint[] splinePoints, int idx)
        {
            var tangent = Vector3.zero;
            var wt = 0f;
            //var idx = i / 3;
            if (idx - 1 >= 0)
            {
                tangent += splinePoints[idx].transform.position - splinePoints[idx - 1].transform.position;
                wt += 1f;
            }
            if (idx + 1 < splinePoints.Length)
            {
                tangent += splinePoints[idx + 1].transform.position - splinePoints[idx].transform.position;
                wt += 1f;
            }
            return tangent / wt;
        }

        static Vector3 TangentBefore(SplinePoint[] splinePoints, int idx)
        {
            var tangent = Vector3.zero;
            var wt = 0f;
            if (idx - 1 >= 0)
            {
                tangent += splinePoints[idx].transform.position - splinePoints[idx - 1].transform.position;
                wt += 1f;
            }
            if (idx + 1 < splinePoints.Length)
            {
                tangent += splinePoints[idx + 1].transform.position - splinePoints[idx].transform.position;
                wt += 1f;
            }
            return tangent / wt;
        }

        static bool UpdateMesh(Transform transform, Vector3[] resultPts0, Vector3[] resultPts1, ref Mesh mesh)
        {
            if (mesh == null)
            {
                mesh = new Mesh();
            }

            var splineLength = 0f;
            for (int i = 1; i < resultPts0.Length; i++)
            {
                splineLength += (resultPts0[i] - resultPts0[i - 1]).magnitude;
            }

            //           \
            //   \   ___--4 uvs1 _-
            //    4--      \
            //     \        \
            //  sp1 3--------3
            //      |        |
            //      2--------2
            //      |        |
            //      1--------1
            //      |        |
            //  sp0 0--------0 uvs1 __
            //      ^        ^
            //     RP0s     RP1s
            //
            var triCount = (resultPts0.Length - 1) * 2;
            var verts = new Vector3[triCount + 2];
            var uvs = new Vector2[triCount + 2];
            var uvs2 = new Vector2[triCount + 2];
            var indices = new int[triCount * 6];
            var distSoFar = 0f;
            for (var i0 = 0; i0 < resultPts0.Length - 1; i0 += 1)
            {
                // Vert indices:
                //
                //     2i1------2i1+1
                //      |\       |
                //      |  \     |
                //      |    \   |
                //      |      \ |
                //     2i0------2i0+1
                //      |        |
                //    sp0--------*
                //
                var i1 = i0 + 1;

                verts[2 * i0] = transform.InverseTransformPoint(resultPts0[i0]);
                verts[2 * i0 + 1] = transform.InverseTransformPoint(resultPts1[i0]);
                verts[2 * i1] = transform.InverseTransformPoint(resultPts0[i1]);
                verts[2 * i1 + 1] = transform.InverseTransformPoint(resultPts1[i1]);

                var axis0 = -new Vector2(resultPts1[i0].x - resultPts0[i0].x, resultPts1[i0].z - resultPts0[i0].z).normalized;
                var axis1 = -new Vector2(resultPts1[i1].x - resultPts0[i1].x, resultPts1[i1].z - resultPts0[i1].z).normalized;
                uvs[2 * i0] = axis0;
                uvs[2 * i0 + 1] = axis0;
                uvs[2 * i1] = axis1;
                uvs[2 * i1 + 1] = axis1;

                // uvs2.x - Dist to closest spline end
                // uvs2.y - 1-0 inverted normalized dist from shoreline
                var nextDistSoFar = distSoFar + (resultPts0[i0 + 1] - resultPts0[i0]).magnitude;
                uvs2[2 * i0].x = uvs2[2 * i0 + 1].x = Mathf.Min(distSoFar, splineLength - distSoFar);
                uvs2[2 * i1].x = uvs2[2 * i1 + 1].x = Mathf.Min(nextDistSoFar, splineLength - nextDistSoFar);
                uvs2[2 * i0].y = uvs[2 * i1].y = 1f;
                uvs2[2 * i0 + 1].y = uvs[2 * i1 + 1].y = 0f;

                indices[i0 * 6] = 2 * i0;
                indices[i0 * 6 + 1] = 2 * i1;
                indices[i0 * 6 + 2] = 2 * i0 + 1;

                indices[i0 * 6 + 3] = 2 * i1;
                indices[i0 * 6 + 4] = 2 * i1 + 1;
                indices[i0 * 6 + 5] = 2 * i0 + 1;

                distSoFar = nextDistSoFar;
            }

            mesh.SetIndices(new int[] { }, MeshTopology.Triangles, 0);
            mesh.vertices = verts;
            mesh.uv = uvs;
            mesh.uv2 = uvs2;
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);
            mesh.RecalculateNormals();

            return true;
        }
    }
}