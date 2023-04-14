using UnityEngine;

public class FroggyCameraFollow : MonoBehaviour
{
    public Transform target; // L'objet que la caméra doit suivre
    public float smoothSpeed = 0.140f; // Vitesse de suivi de la caméra
    public Vector3 offset; // Offset de la caméra par rapport à l'objet
    public float distance = 10f; // Distance de la caméra à la cible

    void LateUpdate()
    {
        if (target != null && target.gameObject.activeSelf)
        {
            Vector3 targetPosition = target.position + offset;
            targetPosition.y = transform.position.y;

            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            Vector3 direction = newPosition - target.position;
            Vector3 flatDirection = Vector3.ProjectOnPlane(direction, Vector3.up);
            flatDirection.z = 0f;
            newPosition = target.position + Vector3.ClampMagnitude(flatDirection, distance);

            // Fixer l'axe Y et l'axe Z de la caméra
            newPosition.y = 1.23f;
            newPosition.z = -32.29f;

            transform.position = newPosition;
        }
    }
}
