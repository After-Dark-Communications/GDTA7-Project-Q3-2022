using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPreviewManager : Manager
{
    [SerializeField]
    [Tooltip("An orderd list of player rendering textures")]
    private List<RenderTexture> playerPreviewTextures = new List<RenderTexture>();
    [SerializeField]
    [Tooltip("An orderd list of player preview cameras")]
    private List<Camera> previewCameras = new List<Camera>();

    public void SetCamOutputToPlayerRenderingTexture(int playerIndex)
    {
        playerIndex = NormalizeIndex(playerIndex);

        Camera camera = previewCameras[playerIndex];
        RenderTexture renderTexture = playerPreviewTextures[playerIndex];

        camera.targetTexture = renderTexture;
    }

    public RenderTexture GetRenderTextureByPlayerIndex(int playerIndex)
    {
        playerIndex = NormalizeIndex(playerIndex);

        return playerPreviewTextures[playerIndex];
    }

    private int NormalizeIndex(int index)
    {
        if (index >= playerPreviewTextures.Count)
            index = playerPreviewTextures.Count - 1;

        if (index < 0)
            index = 0;

        return index;
    }
}
